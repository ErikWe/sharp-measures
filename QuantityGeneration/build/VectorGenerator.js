"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.VectorGenerator = void 0;
const promises_1 = __importDefault(require("fs/promises"));
const Documenter_1 = require("./Documenter");
const Utility_1 = require("./Utility");
class VectorGenerator {
    constructor(destination, documentationDirectory, definitionReader, templateReader) {
        this.destination = destination;
        this.documentationDirectory = documentationDirectory;
        this.definitionReader = definitionReader;
        this.templateReader = templateReader;
    }
    generate() {
        return __awaiter(this, void 0, void 0, function* () {
            const keys = Object.keys(this.definitionReader.definitions.vectors);
            yield Promise.all(keys.map((key) => __awaiter(this, void 0, void 0, function* () {
                for (let dimensionality of this.definitionReader.definitions.vectors[key].dimensionalities) {
                    this.generateVector(this.definitionReader.definitions.vectors[key], dimensionality);
                }
            })));
        });
    }
    generateVector(vector, dimensionality) {
        return __awaiter(this, void 0, void 0, function* () {
            if (!this.fixVectorData(vector)) {
                return;
            }
            let text = this.templateReader.vectorTemplate;
            text = this.setComponentListTexts(vector, dimensionality, text);
            text = this.setConditionalBlocks(vector, dimensionality, text);
            const interfacesText = this.composeInterfacesText(vector);
            text = text.replace(/#Interfaces#/g, interfacesText);
            text = text.replace(/#CommaIfInterface#/g, interfacesText.length > 0 ? ',' : '');
            text = text.replace(/#CommaIfInterfaceOrTransformable#/g, interfacesText.length > 0 || dimensionality == 3 ? ',' : '');
            const convertibleText = this.composeConvertibleText(vector);
            text = text.replace(/#Convertible#/g, convertibleText);
            const quantityToUnitText = this.composeQuantityToUnitText(vector);
            text = text.replace(/#QuantityToUnit#/g, quantityToUnitText);
            const unitsText = this.composeUnitsText(vector);
            text = text.replace(/#Units#/g, unitsText);
            text = this.insertNames(text, vector, dimensionality);
            text = text.replace(/\t/g, '    ');
            if (yield promises_1.default.stat(this.documentationDirectory + '\\Vectors\\' + vector.name + dimensionality + '.txt').catch(() => false)) {
                text = yield Documenter_1.Documenter.document(text, this.documentationDirectory + '\\Vectors\\' + vector.name + dimensionality + '.txt');
            }
            else if (yield promises_1.default.stat(this.documentationDirectory + '\\Vectors\\' + vector.name + '.txt').catch(() => false)) {
                text = yield Documenter_1.Documenter.document(text, this.documentationDirectory + '\\Vectors\\' + vector.name + '.txt');
            }
            else {
                this.reportErrorDocumentationFileNotFound(vector, this.documentationDirectory + '\\Vectors\\' + vector.name + '.txt');
            }
            text = (0, Utility_1.insertAppropriateNewlines)(text, 175);
            text = (0, Utility_1.normalizeLineEndings)(text);
            text = (0, Utility_1.removeConsecutiveNewlines)(text);
            text = (0, Utility_1.normalizeLineEndings)(text);
            yield promises_1.default.writeFile(this.destination + '\\' + vector.name + dimensionality + '.g.cs', text);
        });
    }
    fixVectorData(vector) {
        const requiredEntries = ['name', 'type', 'component', 'baseUnits', 'unit', 'unitBias', 'dimensionalities', 'units', 'convertible'];
        const missingEntries = [];
        for (let requiredEntry of requiredEntries) {
            if (!(requiredEntry in vector)) {
                missingEntries.push(requiredEntry);
            }
        }
        if (missingEntries.length > 0) {
            this.reportErrorMissingEntries(vector, missingEntries);
            return false;
        }
        const redudantEntries = [];
        for (let entry of Object.keys(vector)) {
            if (!requiredEntries.includes(entry)) {
                requiredEntries.push(entry);
            }
        }
        if (redudantEntries.length > 0) {
            this.reportWarningRedundantEntries(vector, redudantEntries);
        }
        vector.component = (0, Utility_1.getVectorComponent)(this.definitionReader.definitions, vector);
        vector.baseUnits = (0, Utility_1.getBaseUnits)(this.definitionReader.definitions, vector);
        vector.unit = (0, Utility_1.getUnitName)(this.definitionReader.definitions, vector);
        vector.unitBias = (0, Utility_1.getUnitBias)(this.definitionReader.definitions, vector);
        vector.dimensionalities = (0, Utility_1.getDimensionalitiesOfVector)(this.definitionReader.definitions, vector);
        vector.units = (0, Utility_1.getUnits)(this.definitionReader.definitions, vector);
        vector.symbol = (0, Utility_1.getSymbol)(this.definitionReader.definitions, vector);
        vector.bases = (0, Utility_1.getBases)(this.definitionReader.definitions, vector);
        vector.convertible = (0, Utility_1.getConvertible)(this.definitionReader.definitions, vector);
        return true;
    }
    insertNames(text, vector, dimensionality) {
        text = text.replace(/#Unit#/g, vector.unit);
        text = text.replace(/#UnitVariable#/g, (0, Utility_1.lowerCase)(vector.unit));
        const unitListTexts = (0, Utility_1.createUnitListTexts)(vector);
        text = text.replace(/#SingularUnits#/g, unitListTexts.singular);
        text = text.replace(/#PluralUnits#/g, unitListTexts.plural);
        text = text.replace(/#Abbreviation#/g, vector.symbol ? vector.symbol : 'SymbolParsingError');
        text = text.replace(/#Component#/g, vector.component ? vector.component : 'Scalar');
        if (vector.component) {
            let componentSquare = (0, Utility_1.getSquare)(this.definitionReader.definitions, this.definitionReader.definitions.scalars[vector.component]);
            if (componentSquare !== false && (!Array.isArray(componentSquare) || componentSquare.length > 0)) {
                text = text.replace(/#SquaredComponent#/g, Array.isArray(componentSquare) ? componentSquare[0] : componentSquare);
            }
        }
        text = text.replace(/#Quantity#/g, vector.name + dimensionality);
        text = text.replace(/#quantity#/g, (0, Utility_1.lowerCase)(vector.name + dimensionality));
        text = text.replace(/#Dimensionality#/g, dimensionality.toString());
        return text;
    }
    setComponentListTexts(vector, dimensionality, text) {
        const componentLists = [{
                replace: '#ComponentListProperties#',
                append: (name) => '\t#Document:Component' + name + '(#Quantity#, #Dimensionality#, #Unit#, #PluralUnits#)#\n' +
                    '\tpublic double ' + name + ' { get; init; }\n',
                slice: (result) => result.slice(0, -1)
            }, {
                replace: '#ComponentListAssignment#',
                append: (name) => '\t\t' + name + ' = ' + (0, Utility_1.lowerCase)(name) + ';\n',
                slice: (result) => result.slice(0, -1)
            }, {
                replace: '#ComponentListComponents#',
                append: (name) => name + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListZero#',
                append: (name) => '0, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListScalarQuantity#',
                append: (name) => vector.component + ' ' + (0, Utility_1.lowerCase)(name) + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListScalar#',
                append: (name) => 'Scalar ' + (0, Utility_1.lowerCase)(name) + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListDouble#',
                append: (name) => 'double ' + (0, Utility_1.lowerCase)(name) + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListTupleAccess#',
                append: (name) => 'components.' + (0, Utility_1.lowerCase)(name) + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorAccess#',
                append: (name) => 'components.' + name + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListAAccess#',
                append: (name) => 'a.' + name + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListLowerCaseMagnitudes#',
                append: (name) => (0, Utility_1.lowerCase)(name) + '.Magnitude, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListMagnitudeFromUnit#',
                append: vector.unitBias ?
                    (name) => '(' + (0, Utility_1.lowerCase)(name) + ' * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale, ' :
                    (name) => (0, Utility_1.lowerCase)(name) + ' * #UnitVariable#.Factor, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListFormatting#',
                append: (name) => '{' + name + '}, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListNegate#',
                append: (name) => '-' + name + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListNegateA#',
                append: (name) => '-a.' + name + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListRemainder#',
                append: (name) => name + ' % divisor.Magnitude, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListMultiplication#',
                append: (name) => name + ' * factor.Magnitude, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListDivision#',
                append: (name) => name + ' / divisor.Magnitude, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorARemainderScalarB#',
                append: (name) => 'a.' + name + ' % b.Magnitude, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorATimesScalarB#',
                append: (name) => 'a.' + name + ' * b.Magnitude, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListScalarATimesVectorB#',
                append: (name) => 'a.Magnitude * b.' + name + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorADividedByScalarB#',
                append: (name) => 'a.' + name + ' / b.Magnitude, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListRemainderDouble#',
                append: (name) => name + ' % divisor, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListMultiplicationDouble#',
                append: (name) => name + ' * factor, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListDivisionDouble#',
                append: (name) => name + ' / divisor, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorARemainderDoubleB#',
                append: (name) => 'a.' + name + ' % b, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorATimesDoubleB#',
                append: (name) => 'a.' + name + ' * b, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListDoubleATimesVectorB#',
                append: (name) => 'a * b.' + name + ', ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorADividedByDoubleB#',
                append: (name) => 'a.' + name + ' / b, ',
                slice: (result) => result.slice(0, -2)
            }, {
                replace: '#ComponentListUnnamedDoubles#',
                append: (name) => 'double, ',
                slice: (result) => result.slice(0, -2)
            }
        ];
        for (let componentList of componentLists) {
            let listText = '';
            for (let componentName of (0, Utility_1.getVectorComponentNames)(dimensionality)) {
                listText += componentList.append(componentName);
            }
            text = text.replace(new RegExp(componentList.replace, 'g'), componentList.slice(listText));
        }
        return text;
    }
    setConditionalBlocks(vector, dimensionality, text) {
        if (vector.component) {
            text = text.replace(/(?:\n|\r\n|\r)#ScalarQuantityComponent#/g, '');
            text = text.replace(/(?:\n|\r\n|\r)#\/ScalarQuantityComponent#/g, '');
            text = text.replace(/(?:\n|\r\n|\r)#NonScalarQuantityComponent#([^]+?)(?:\n|\r\n|\r)#\/NonScalarQuantityComponent#/g, '');
        }
        else {
            text = text.replace(/(?:\n|\r\n|\r)#NonScalarQuantityComponent#/g, '');
            text = text.replace(/(?:\n|\r\n|\r)#\/NonScalarQuantityComponent#/g, '');
            text = text.replace(/(?:\n|\r\n|\r)#ScalarQuantityComponent#([^]+?)(?:\n|\r\n|\r)#\/ScalarQuantityComponent#/g, '');
        }
        const emptyArrayOrFalse = (obj) => {
            return (obj === false || Array.isArray(obj) && obj.length === 0);
        };
        if (vector.component && !emptyArrayOrFalse(this.definitionReader.definitions.scalars[vector.component].square)) {
            text = text.replace(/(?:\n|\r\n|\r)#SquaredScalarQuantityComponent#/g, '');
            text = text.replace(/(?:\n|\r\n|\r)#\/SquaredScalarQuantityComponent#/g, '');
            text = text.replace(/(?:\n|\r\n|\r)#NonSquaredScalarQuantityComponent#([^]+?)(?:\n|\r\n|\r)#\/NonSquaredScalarQuantityComponent#/g, '');
        }
        else {
            text = text.replace(/(?:\n|\r\n|\r)#NonSquaredScalarQuantityComponent#/g, '');
            text = text.replace(/(?:\n|\r\n|\r)#\/NonSquaredScalarQuantityComponent#/g, '');
            text = text.replace(/(?:\n|\r\n|\r)#SquaredScalarQuantityComponent#([^]+?)(?:\n|\r\n|\r)#\/SquaredScalarQuantityComponent#/g, '');
        }
        if (dimensionality === 3) {
            text = text.replace(/(?:\n|\r\n|\r)#Vector3#/g, '');
            text = text.replace(/(?:\n|\r\n|\r)#\/Vector3#/g, '');
        }
        else {
            text = text.replace(/(?:\n|\r\n|\r)#Vector3#([^]+?)(?:\n|\r\n|\r)#\/Vector3#/g, '');
        }
        return text;
    }
    composeInterfacesText(vector) {
        const interfaceTexts = [];
        interfaceTexts.push('IMultiplicableVector#Dimensionality#Quantity<#Quantity#, Scalar>');
        interfaceTexts.push('IMultiplicableVector#Dimensionality#Quantity<Unhandled3, Unhandled>');
        interfaceTexts.push('IDivisibleVector#Dimensionality#Quantity<#Quantity#, Scalar>');
        interfaceTexts.push('IDivisibleVector#Dimensionality#Quantity<Unhandled3, Unhandled>');
        if (vector.component) {
            interfaceTexts.push('IDotableVector#Dimensionality#Quantity<#Component#, Vector3>');
        }
        interfaceTexts.push('IDotableVector#Dimensionality#Quantity<Unhandled, Unhandled#Dimensionality#>');
        interfaceTexts.push('ICrossableVector#Dimensionality#Quantity<#Quantity#, Vector#Dimensionality#>');
        interfaceTexts.push('ICrossableVector#Dimensionality#Quantity<Unhandled#Dimensionality#, Unhandled#Dimensionality#>');
        interfaceTexts.push('IGenericallyMultiplicableVector#Dimensionality#Quantity');
        interfaceTexts.push('IGenericallyDivisibleVector#Dimensionality#Quantity');
        interfaceTexts.push('IGenericallyDotableVector#Dimensionality#Quantity');
        interfaceTexts.push('IGenericallyCrossableVector#Dimensionality#Quantity');
        let interfacesText = "";
        for (let interfaceText of interfaceTexts) {
            interfacesText += '\t' + interfaceText + ',\n';
        }
        return interfacesText.slice(0, -2);
    }
    composeConvertibleText(vector) {
        let convertibleText = '';
        if (Array.isArray(vector.convertible)) {
            for (let convertible of vector.convertible) {
                for (let dimensionality of vector.dimensionalities) {
                    if (this.definitionReader.definitions.vectors[convertible].dimensionalities.includes(dimensionality)) {
                        convertibleText += '\t#Document:AsShared(#Quantity#, #Dimensionality#, ' + convertible + ')#\n';
                        convertibleText += '\tpublic ' + convertible + '#Dimensionality# As' + convertible + '#Dimensionality#() => new(';
                        for (let name of (0, Utility_1.getVectorComponentNames)(dimensionality)) {
                            convertibleText += name + ', ';
                        }
                    }
                    convertibleText = convertibleText.slice(0, -2) + ');\n';
                }
            }
        }
        return convertibleText.slice(0, -1);
    }
    composeQuantityToUnitText(vector) {
        if (vector.unitBias) {
            return '(#quantity#.ToVector#Dimensionality#() / #UnitVariable#.BaseScale - Vector#Dimensionality#.Ones * #UnitVariable#.Bias) / #UnitVariable#.Prefix.Scale';
        }
        else {
            return '#quantity#.ToVector#Dimensionality#() / #UnitVariable#.Factor';
        }
    }
    composeUnitsText(vector) {
        let unitsText = "";
        if (Array.isArray(vector.units)) {
            for (let unit of vector.units) {
                if (unit.special && unit.separator === true) {
                    unitsText += '\n';
                }
                else if (!unit.special) {
                    unitsText += '\t#Document:InUnit(quantity = #Quantity#, dimensionality = #Dimensionality#, unit = #Unit#, unitName = ' + unit.singular + ')#\n';
                    unitsText += '\tpublic Vector#Dimensionality# ' + unit.plural + ' => ';
                    unitsText += 'InUnit(' + vector.unit + '.' + unit.singular + ');\n';
                }
            }
        }
        return unitsText;
    }
    reportErrorMissingEntries(vector, entries) {
        console.error('Vector quantity: [' + vector.name + '] is missing ' + (entries.length > 1 ? 'entries' : 'entry') + ': ' + entries + '.');
    }
    reportErrorDocumentationFileNotFound(vector, fileName) {
        console.error('Could not locate documentation file for vector quantity: [' + vector.name + '], tried: [' + fileName + '].');
    }
    reportWarningRedundantEntries(vector, entries) {
        console.warn('Vector quantity: [' + vector.name + '] has redundant ' + (entries.length > 1 ? 'entries' : 'entry') + ':' + entries + '.');
    }
}
exports.VectorGenerator = VectorGenerator;
//# sourceMappingURL=VectorGenerator.js.map