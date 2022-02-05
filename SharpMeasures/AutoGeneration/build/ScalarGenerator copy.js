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
exports.ScalarGenerator = void 0;
const promises_1 = __importDefault(require("fs/promises"));
const Documenter_1 = require("./Documenter");
const Utility_1 = require("./Utility");
class ScalarGenerator {
    constructor(destination, documentationDirectory, definitionReader, templateReader) {
        this.destination = destination;
        this.documentationDirectory = documentationDirectory;
        this.definitionReader = definitionReader;
        this.templateReader = templateReader;
    }
    generate() {
        return __awaiter(this, void 0, void 0, function* () {
            const keys = Object.keys(this.definitionReader.definitions.scalars);
            yield Promise.all(keys.map((key) => __awaiter(this, void 0, void 0, function* () {
                this.generateScalar(this.definitionReader.definitions.scalars[key]);
            })));
        });
    }
    generateScalar(scalar) {
        return __awaiter(this, void 0, void 0, function* () {
            if (!this.fixScalarData(scalar)) {
                return;
            }
            let text = this.templateReader.scalarTemplate;
            const interfacesText = this.composeInterfacesText(scalar);
            text = text.replace(new RegExp('#Interfaces#', 'g'), interfacesText);
            text = text.replace(new RegExp('#CommaIfInterface#', 'g'), interfacesText.length > 0 ? ',' : '');
            const basesText = this.composeBasesText(scalar);
            text = text.replace(new RegExp('#Bases#', 'g'), basesText);
            const fromText = this.composeFromText(scalar);
            text = text.replace(new RegExp('#From#', 'g'), fromText);
            const unitsText = this.composeUnitsText(scalar);
            text = text.replace(new RegExp('#Units#', 'g'), unitsText);
            const powersText = this.composePowersText(scalar);
            text = text.replace(new RegExp('#Powers#', 'g'), powersText);
            const invertDoubleText = this.composeInversionOperatorDoubleText(scalar);
            text = text.replace(new RegExp('#InversionOperatorDouble#', 'g'), invertDoubleText);
            const invertScalarText = this.composeInversionOperatorScalarText(scalar);
            text = text.replace(new RegExp('#InversionOperatorScalar#', 'g'), invertScalarText);
            const magnitudeFromUnitDoubleText = this.composeMagnitudeFromUnitDoubleText(scalar);
            text = text.replace(new RegExp('#MagnitudeFromUnitDouble#', 'g'), magnitudeFromUnitDoubleText);
            const magnitudeFromUnitScalarText = this.composeMagnitudeFromUnitScalarText(scalar);
            text = text.replace(new RegExp('#MagnitudeFromUnitScalar#', 'g'), magnitudeFromUnitScalarText);
            const quantityToUnitText = this.composeQuantityToUnitText(scalar);
            text = text.replace(new RegExp('#QuantityToUnit#', 'g'), quantityToUnitText);
            const convertibleText = this.composeConvertibleText(scalar);
            text = text.replace(new RegExp('#Convertible#', 'g'), convertibleText);
            const vectorText = this.composeToVectorText(scalar);
            text = text.replace(new RegExp('#ToVector#', 'g'), vectorText);
            text = this.insertNames(text, scalar);
            text = text.replace(new RegExp('\t', 'g'), '    ');
            text = yield Documenter_1.Documenter.document(text, this.documentationDirectory + '\\Scalars\\' + scalar.name + '.txt');
            text = (0, Utility_1.insertAppropriateNewlines)(text);
            text = (0, Utility_1.removeConsecutiveNewlines)(text);
            text = (0, Utility_1.normalizeLineEndings)(text);
            yield promises_1.default.writeFile(this.destination + '\\' + scalar.name + '.g.cs', text);
        });
    }
    fixScalarData(scalar) {
        const requiredEntries = ['name', 'type', 'baseUnits', 'unit', 'unitBias',
            'vector', 'inverse', 'square', 'cube', 'squareRoot', 'cubeRoot', 'units', 'convertible'];
        const optionalEntries = [];
        const missingEntries = [];
        for (let requiredEntry of requiredEntries) {
            if (!(requiredEntry in scalar)) {
                missingEntries.push(requiredEntry);
            }
        }
        if (missingEntries.length > 0) {
            console.log('Scalar quantity: [' + scalar.name + '] is missing ' + (missingEntries.length > 1 ? 'entries' : 'entry') + ': ' + missingEntries + '.');
            return false;
        }
        const redudantEntries = [];
        for (let entry of Object.keys(scalar)) {
            if (!(entry in requiredEntries || entry in optionalEntries)) {
                requiredEntries.push(entry);
            }
        }
        if (redudantEntries.length > 0) {
            console.log('Scalar quantity: [' + scalar.name + '] has redundant ' + (redudantEntries.length > 1 ? 'entries' : 'entry') + ':' + redudantEntries + '.');
        }
        scalar.baseUnits = (0, Utility_1.getBaseUnits)(this.definitionReader.definitions, scalar);
        scalar.unit = (0, Utility_1.getUnitName)(this.definitionReader.definitions, scalar);
        scalar.unitBias = (0, Utility_1.getUnitBias)(this.definitionReader.definitions, scalar);
        scalar.vector = (0, Utility_1.getNameOfVectorVersionOfScalar)(this.definitionReader.definitions, scalar);
        scalar.inverse = (0, Utility_1.getInverse)(this.definitionReader.definitions, scalar);
        scalar.square = (0, Utility_1.getSquare)(this.definitionReader.definitions, scalar);
        scalar.cube = (0, Utility_1.getCube)(this.definitionReader.definitions, scalar);
        scalar.squareRoot = (0, Utility_1.getSquareRoot)(this.definitionReader.definitions, scalar);
        scalar.cubeRoot = (0, Utility_1.getCubeRoot)(this.definitionReader.definitions, scalar);
        scalar.units = (0, Utility_1.getUnits)(this.definitionReader.definitions, scalar);
        scalar.symbol = (0, Utility_1.getSymbol)(this.definitionReader.definitions, scalar);
        scalar.bases = (0, Utility_1.getBases)(this.definitionReader.definitions, scalar);
        scalar.convertible = (0, Utility_1.getConvertible)(this.definitionReader.definitions, scalar);
        if (scalar.vector) {
            scalar.vectorDimensionalities = (0, Utility_1.getDimensionalitiesOfVector)(this.definitionReader.definitions, this.definitionReader.definitions.vectors[scalar.vector]);
        }
        else {
            scalar.vectorDimensionalities = [];
        }
        if (!(typeof scalar.units === 'string')) {
            let siFound = false;
            for (let i = 0; i < scalar.units.length; i++) {
                const unit = scalar.units[i];
                if (unit.special) {
                    continue;
                }
                if (unit.SI) {
                    siFound = true;
                }
                unit.plural = (0, Utility_1.unitPlural)(unit.singular, unit.plural);
            }
            if (!siFound) {
                console.error('Scalar quantity: [' + scalar.name + '] does not have a unit marked as the SI unit, using the entry [SI].');
                return false;
            }
        }
        if (!(typeof scalar.bases === 'string')) {
            for (let i = 0; i < scalar.bases.length; i++) {
                const base = scalar.bases[i];
                if (base.special) {
                    continue;
                }
                base.plural = (0, Utility_1.unitPlural)(base.singular, base.plural);
            }
        }
        return true;
    }
    insertNames(text, scalar) {
        text = text.replace(new RegExp('#Unit#', 'g'), scalar.unit);
        text = text.replace(new RegExp('#UnitVariable#', 'g'), (0, Utility_1.lowerCase)(scalar.unit));
        const unitListTexts = (0, Utility_1.createUnitListTexts)(scalar);
        text = text.replace(new RegExp('#SingularUnits#', 'g'), unitListTexts.singular);
        text = text.replace(new RegExp('#PluralUnits#', 'g'), unitListTexts.plural);
        const powers = [
            { name: 'Inverse', data: scalar.inverse },
            { name: 'Square', data: scalar.square },
            { name: 'Cube', data: scalar.cube },
            { name: 'SquareRoot', data: scalar.squareRoot },
            { name: 'CubeRoot', data: scalar.cubeRoot }
        ];
        for (let power of powers) {
            if (power.data && power.data.length > 0) {
                text = text.replace(new RegExp('#' + power.name + 'Quantity#', 'g'), power.data[0]);
                text = text.replace(new RegExp('#' + power.name + 'QuantityVariable#', 'g'), (0, Utility_1.lowerCase)(power.data[0]));
                for (let i = 0; i < power.data.length; i++) {
                    const quantity = power.data[i];
                    text = text.replace(new RegExp('#' + power.name + 'Quantity' + i + '#', 'g'), quantity);
                    text = text.replace(new RegExp('#' + power.name + 'Quantity' + i + 'Variable#', 'g'), (0, Utility_1.lowerCase)(quantity));
                }
            }
        }
        if (scalar.vector) {
            text = text.replace(new RegExp('#VectorQuantity#', 'g'), scalar.vector);
        }
        if (scalar.symbol) {
            text = text.replace(new RegExp('#Abbreviation#', 'g'), scalar.symbol);
        }
        text = text.replace(new RegExp('#Quantity#', 'g'), scalar.name);
        text = text.replace(new RegExp('#quantity#', 'g'), (0, Utility_1.lowerCase)(scalar.name));
        return text;
    }
    composeInterfacesText(scalar) {
        const powers = [
            { name: 'Inverse', data: scalar.inverse, expression: (x) => 'IInvertibleScalarQuantity<' + x + '>' },
            { name: 'Square', data: scalar.square, expression: (x) => 'ISquarableScalarQuantity<' + x + '>' },
            { name: 'Cube', data: scalar.cube, expression: (x) => 'ICubableScalarQuantity<' + x + '>' },
            { name: 'SquareRoot', data: scalar.squareRoot, expression: (x) => 'ISquareRootableScalarQuantity<' + x + '>' },
            { name: 'CubeRoot', data: scalar.cubeRoot, expression: (x) => 'ICubeRootableScalarQuantity<' + x + '>' }
        ];
        const interfaces = [];
        for (let power of powers) {
            if (power.data && power.data.length > 0) {
                interfaces.push(power.expression('#' + power.name + 'Quantity#'));
            }
        }
        interfaces.push('IMultiplicableScalarQuantity<#Quantity#, Scalar>');
        interfaces.push('IMultiplicableScalarQuantity<Unhandled, Unhandled>');
        interfaces.push('IDivisibleScalarQuantity<#Quantity#, Scalar>');
        interfaces.push('IDivisibleScalarQuantity<Unhandled, Unhandled>');
        interfaces.push('IGenericallyMultiplicableScalarQuantity');
        interfaces.push('IGenericallyDivisibleScalarQuantity');
        if (scalar.vectorDimensionalities) {
            for (let dimensionality of scalar.vectorDimensionalities) {
                interfaces.push('IVector' + dimensionality + 'MultiplicableScalarQuantity<#VectorQuantity#' + dimensionality + ', Vector' + dimensionality + '>');
            }
        }
        let interfacesText = '';
        for (let _interface of interfaces) {
            interfacesText += '\t' + _interface + ',\n';
        }
        return interfacesText.slice(0, -2);
    }
    composeBasesText(scalar) {
        let basesText = '';
        if (Array.isArray(scalar.bases)) {
            for (let base of scalar.bases) {
                if (base.special) {
                    if (base.separator) {
                        basesText += '\n';
                    }
                }
                else {
                    basesText += '\t#Document:OneUnit(#Quantity#, #Unit#, ' + base.singular + ')#\n';
                    basesText += '\tpublic static #Quantity# One' + base.singular + ' { get; } = new(1, #Unit#.' + base.singular + ');\n';
                }
            }
        }
        return basesText;
    }
    composeFromText(scalar) {
        const powers = [
            { name: 'Inverse', data: scalar.inverse, expression: (x) => '1 / ' + x + '.Magnitude' },
            { name: 'Square', data: scalar.square, expression: (x) => 'Math.Sqrt(' + x + '.Magnitude)' },
            { name: 'Cube', data: scalar.cube, expression: (x) => 'Math.Cbrt(' + x + '.Magnitude)' },
            { name: 'SquareRoot', data: scalar.squareRoot, expression: (x) => 'Math.Pow(' + x + '.Magnitude, 2)' },
            { name: 'CubeRoot', data: scalar.cubeRoot, expression: (x) => 'Math.Pow(' + x + '.Magnitude, 3)' }
        ];
        let fromText = '';
        for (let power of powers) {
            if (Array.isArray(power.data)) {
                for (let i = 0; i < power.data.length; i++) {
                    const quantity = power.name + 'Quantity' + i;
                    const variable = quantity + 'Variable';
                    fromText += '\t#Document:From' + power.name + '(#Quantity#, #' + quantity + '#, #' + variable + '#)#\n';
                    fromText += '\tpublic static #Quantity# From(#' + quantity + '# #' + variable + '#) => new(' + power.expression('#' + variable + '#') + ');\n';
                }
            }
        }
        return fromText;
    }
    composeUnitsText(scalar) {
        let unitsText = '';
        if (Array.isArray(scalar.units)) {
            for (let unit of scalar.units) {
                if (unit.special) {
                    if (unit.separator) {
                        unitsText += '\n';
                    }
                }
                else {
                    unitsText += '\t#Document:InUnit(#Quantity#, #Unit#, ' + unit.singular + ')#\n';
                    unitsText += '\tpublic Scalar In' + unit.plural + ' => InUnit(#Unit#.' + unit.singular + ');\n';
                }
            }
        }
        return unitsText;
    }
    composePowersText(scalar) {
        const powers = [
            { powerName: 'Inverse', data: scalar.inverse, methodName: 'Invert' },
            { powerName: 'Square', data: scalar.square, methodName: 'Square' },
            { powerName: 'Cube', data: scalar.cube, methodName: 'Cube' },
            { powerName: 'SquareRoot', data: scalar.squareRoot, methodName: 'SquareRoot' },
            { powerName: 'CubeRoot', data: scalar.cubeRoot, methodName: 'CubeRoot' }
        ];
        let powersText = '';
        for (let power of powers) {
            if (power.data && power.data.length > 0) {
                const quantity = power.powerName + 'Quantity';
                powersText += '\t#Document:' + power.methodName + '(#Quantity#, #' + quantity + '#)#\n';
                powersText += '\tpublic #' + quantity + '# ' + power.methodName + '() => #' + quantity + '#.From(this);\n';
            }
        }
        return powersText;
    }
    composeInversionOperatorDoubleText(scalar) {
        let invertText = '';
        if (scalar.inverse && scalar.inverse.length > 0) {
            invertText += '\#Document:DivideDoubleOperatorRHS(#Quantity#, #InverseQuantity#)#\n';
            invertText += '\tpublic static #InverseQuantity# operator /(double x, #Quantity# y) => x * y.Invert();\n';
        }
        return invertText;
    }
    composeInversionOperatorScalarText(scalar) {
        let invertText = '';
        if (scalar.inverse && scalar.inverse.length > 0) {
            invertText += '\#Document:DivideScalarOperatorRHS(#Quantity#, #InverseQuantity#)#\n';
            invertText += '\tpublic static #InverseQuantity# operator /(Scalar x, #Quantity# y) => x * y.Invert();\n';
        }
        return invertText;
    }
    composeMagnitudeFromUnitDoubleText(scalar) {
        if (scalar.unitBias === true) {
            return '(magnitude * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale';
        }
        else {
            return 'magnitude * #UnitVariable#.Factor';
        }
    }
    composeMagnitudeFromUnitScalarText(scalar) {
        if (scalar.unitBias === true) {
            return '(magnitude.Magnitude * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale';
        }
        else {
            return 'magnitude.Magnitude * #UnitVariable#.Factor';
        }
    }
    composeQuantityToUnitText(scalar) {
        if (scalar.unitBias === true) {
            return '(#quantity#.Magnitude / #UnitVariable#.BaseScale - #UnitVariable#.Bias) / #UnitVariable#.Prefix.Scale';
        }
        else {
            return '#quantity#.Magnitude / #UnitVariable#.Factor';
        }
    }
    composeConvertibleText(scalar) {
        let convertibleText = '';
        if (Array.isArray(scalar.convertible)) {
            for (let convertible of scalar.convertible) {
                convertibleText += '\t#Document:AsShared(quantity = #Quantity#, sharedQuantity = ' + convertible + ')#\n';
                convertibleText += '\tpublic ' + convertible + ' As' + convertible + ' => new(Magnitude);\n';
            }
        }
        return convertibleText;
    }
    composeToVectorText(scalar) {
        let toVectorMethods = '';
        let toVectorOperations = '';
        if (Array.isArray(scalar.vectorDimensionalities)) {
            for (let dimensionality of scalar.vectorDimensionalities) {
                const argument = '(quantity = #Quantity#, vectorQuantity = #VectorQuantity#, n = #Dimensionality#)';
                let tupleDefinition = '';
                for (let componentName of (0, Utility_1.getVectorComponentNames)(dimensionality)) {
                    tupleDefinition += 'double ' + (0, Utility_1.lowerCase)(componentName) + ', ';
                }
                tupleDefinition = '(' + tupleDefinition.slice(0, -2) + ')';
                toVectorMethods += '\t#Document:MultiplyVectorNMethod' + argument + '#\n';
                toVectorMethods += '\tpublic #VectorQuantity##Dimensionality# Multiply(Vector#Dimensionality# vector) #newline#=> new(vector * Magnitude);\n';
                toVectorMethods += '\t#Document:MultiplyTupleNMethod' + argument + '#\n';
                toVectorMethods += '\tpublic #VectorQuantity##Dimensionality# Multiply(#newline#' + tupleDefinition + ' components) #newline#';
                toVectorMethods += '=> Multiply(new Vector#Dimensionality#(components));\n';
                toVectorMethods += '\t#Document:MultiplyScalarTupleNMethod' + argument + '#\n';
                toVectorMethods += '\tpublic #VectorQuantity##Dimensionality# Multiply(#newline#' + tupleDefinition + ' components) #newline#';
                toVectorMethods += '=> Multiply(new Vector#Dimensionality#(components));\n';
                toVectorOperations += '\t#Document:MultiplyVectorNOperatorLHS' + argument + '#\n';
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(#Quantity# a, Vector#Dimensionality# b) #newline#=> a.Multiply(b);\n';
                toVectorOperations += '\t#Document:MultiplyVectorNOperatorRHS' + argument + '#\n';
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(Vector#Dimensionality# a, #Quantity# b) #newline#=> b.Multiply(a);\n';
                toVectorOperations += '\t#Document:MultiplyTupleNOperatorLHS' + argument + '#\n';
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(#Quantity# a, ' + tupleDefinition + ' b) #newline#=> a.Multiply(b);\n';
                toVectorOperations += '\t#Document:MultiplyTupleNOperatorRHS' + argument + '#\n';
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(' + tupleDefinition + ' a, #Quantity# b) #newline#=> b.Multiply(a);\n';
                toVectorOperations += '\t#Document:MultiplyScalarTupleNOperatorLHS' + argument + '#\n';
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(#Quantity# a, ' + tupleDefinition + ' b) #newline#=> a.Multiply(b);\n';
                toVectorOperations += '\t#Document:MultiplyScalarTupleNOperatorRHS' + argument + '#\n';
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(' + tupleDefinition + ' a, #Quantity# b) #newline#=> b.Multiply(a);\n';
                toVectorMethods = toVectorMethods.replace(new RegExp('#Dimensionality#', 'g'), dimensionality.toString());
                toVectorOperations = toVectorOperations.replace(new RegExp('#Dimensionality#', 'g'), dimensionality.toString());
            }
        }
        return toVectorMethods + toVectorOperations;
    }
}
exports.ScalarGenerator = ScalarGenerator;
//# sourceMappingURL=ScalarGenerator%20copy.js.map