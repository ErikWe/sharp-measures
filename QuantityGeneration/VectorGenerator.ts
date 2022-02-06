import fsp from 'fs/promises'
import { Documenter } from './Documenter'
import { DefinitionReader } from './DefinitionReader'
import { VectorQuantity } from './VectorQuantity'
import { TemplateReader } from './TemplateReader'
import { createUnitListTexts, getBases, getBaseUnits, getConvertible, getDimensionalitiesOfVector, getSquare, getSymbol, getUnitBias, getUnitName,
    getUnits, getVectorComponent, getVectorComponentNames, insertAppropriateNewlines, lowerCase, normalizeLineEndings, removeConsecutiveNewlines } from './Utility'

export class VectorGenerator {

    public constructor(private destination: string, private documentationDirectory: string,
        private definitionReader: DefinitionReader, private templateReader: TemplateReader) {}

    public async generate(): Promise<void> {
        const keys: string[] = Object.keys(this.definitionReader.definitions.vectors)

        await Promise.all(keys.map(async (key: string) => {
            for (let dimensionality of this.definitionReader.definitions.vectors[key].dimensionalities) {
                this.generateVector(this.definitionReader.definitions.vectors[key], dimensionality)
            }
        }))
    }

    private async generateVector(vector: VectorQuantity, dimensionality: number): Promise<void> {
        if (!this.fixVectorData(vector)) {
            return
        }

        let text: string = this.templateReader.vectorTemplate

        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.setConditionalBlocks(vector, dimensionality, text)

        const interfacesText: string = this.composeInterfacesText(vector)
        text = text.replace(/#Interfaces#/g, interfacesText)
        text = text.replace(/#CommaIfInterface#/g, interfacesText.length > 0 ? ',' : '')
        text = text.replace(/#CommaIfInterfaceOrTransformable#/g, interfacesText.length > 0 || dimensionality == 3 ? ',' : '')

        const convertibleText: string = this.composeConvertibleText(vector)
        text = text.replace(/#Convertible#/g, convertibleText)

        const quantityToUnitText: string = this.composeQuantityToUnitText(vector)
        text = text.replace(/#QuantityToUnit#/g, quantityToUnitText)

        const unitsText: string = this.composeUnitsText(vector)
        text = text.replace(/#Units#/g, unitsText)

        text = this.insertNames(text, vector, dimensionality)

        text = text.replace(/\t/g, '    ')

        if (await fsp.stat(this.documentationDirectory + '\\Vectors\\' + vector.name + dimensionality + '.txt').catch(() => false)) {
            text = await Documenter.document(text, this.documentationDirectory + '\\Vectors\\' + vector.name + dimensionality + '.txt')
        } else if (await fsp.stat(this.documentationDirectory + '\\Vectors\\' + vector.name + '.txt').catch(() => false)) {
            text = await Documenter.document(text, this.documentationDirectory + '\\Vectors\\' + vector.name + '.txt')
        } else {
            this.reportErrorDocumentationFileNotFound(vector, this.documentationDirectory + '\\Vectors\\' + vector.name + '.txt')
        }

        text = insertAppropriateNewlines(text, 175)
        text = normalizeLineEndings(text)
        text = removeConsecutiveNewlines(text)
        text = normalizeLineEndings(text)

        await fsp.writeFile(this.destination + '\\' + vector.name + dimensionality + '.g.cs', text)
    }

    private fixVectorData(vector: VectorQuantity): boolean {
        const requiredEntries: string[] = ['name', 'type', 'component', 'baseUnits', 'unit', 'unitBias', 'dimensionalities', 'units', 'convertible']

        const missingEntries: string[] = []
        for (let requiredEntry of requiredEntries) {
            if (!(requiredEntry in vector)) {
                missingEntries.push(requiredEntry)
            }
        }

        if (missingEntries.length > 0) {
            this.reportErrorMissingEntries(vector, missingEntries)
            return false
        }

        const redudantEntries: string[] = []
        for (let entry of Object.keys(vector)) {
            if (!requiredEntries.includes(entry))
            {
                requiredEntries.push(entry)
            }
        }

        if (redudantEntries.length > 0) {
            this.reportWarningRedundantEntries(vector, redudantEntries)
        }

        vector.component = getVectorComponent(this.definitionReader.definitions, vector)
        vector.baseUnits = getBaseUnits(this.definitionReader.definitions, vector)
        vector.unit = getUnitName(this.definitionReader.definitions, vector)
        vector.unitBias = getUnitBias(this.definitionReader.definitions, vector)
        vector.dimensionalities = getDimensionalitiesOfVector(this.definitionReader.definitions, vector)
        vector.units = getUnits(this.definitionReader.definitions, vector)
        vector.symbol = getSymbol(this.definitionReader.definitions, vector)
        vector.bases = getBases(this.definitionReader.definitions, vector)
        vector.convertible = getConvertible(this.definitionReader.definitions, vector)

        return true
    }

    private insertNames(text: string, vector: VectorQuantity, dimensionality: number): string {
        text = text.replace(/#Unit#/g, vector.unit)
        text = text.replace(/#UnitVariable#/g, lowerCase(vector.unit))

        const unitListTexts = createUnitListTexts(vector)
        text = text.replace(/#SingularUnits#/g, unitListTexts.singular)
        text = text.replace(/#PluralUnits#/g, unitListTexts.plural)
        
        text = text.replace(/#Abbreviation#/g, vector.symbol ? vector.symbol : 'SymbolParsingError')

        text = text.replace(/#Component#/g, vector.component ? vector.component : 'Scalar')

        if (vector.component) {
            let componentSquare: string | string[] | false = getSquare(this.definitionReader.definitions, this.definitionReader.definitions.scalars[vector.component])
            if (componentSquare !== false && (!Array.isArray(componentSquare) || componentSquare.length > 0)) {
                text = text.replace(/#SquaredComponent#/g, Array.isArray(componentSquare) ? componentSquare[0] : componentSquare)
            }
        }

        text = text.replace(/#Quantity#/g, vector.name + dimensionality)
        text = text.replace(/#quantity#/g, lowerCase(vector.name + dimensionality))
        text = text.replace(/#Dimensionality#/g, dimensionality.toString())
        return text
    }

    private setComponentListTexts(vector: VectorQuantity, dimensionality: number, text: string): string {
        const componentLists: { replace: string, append: (name: string) => string, slice: (text: string) => string }[] = [{
                replace: '#ComponentListProperties#',
                append: (name: string) =>
                    '\t#Document:Component' + name + '(#Quantity#, #Dimensionality#, #Unit#, #PluralUnits#)#\n' +
                    '\tpublic double ' + name + ' { get; init; }\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#ComponentListAssignment#',
                append: (name: string) => '\t\t' + name + ' = ' + lowerCase(name) + ';\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#ComponentListComponents#',
                append: (name: string) => name + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListZero#',
                append: (name: string) => '0, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListScalarQuantity#',
                append: (name: string) => vector.component + ' ' + lowerCase(name) + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListScalar#',
                append: (name: string) => 'Scalar ' +  lowerCase(name) + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListDouble#',
                append: (name: string) => 'double ' + lowerCase(name) + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListTupleAccess#',
                append: (name: string) => 'components.' + lowerCase(name) + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorAccess#',
                append: (name: string) => 'components.' + name + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListAAccess#',
                append: (name: string) => 'a.' + name + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListLowerCaseMagnitudes#',
                append: (name: string) => lowerCase(name) + '.Magnitude, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListMagnitudeFromUnit#',
                append: vector.unitBias ?
                    (name: string) => '(' + lowerCase(name) + ' * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale, ' :
                    (name: string) => lowerCase(name) + ' * #UnitVariable#.Factor, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListFormatting#',
                append: (name: string) => '{' + name + '}, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListNegate#',
                append: (name: string) => '-' + name + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListNegateA#',
                append: (name: string) => '-a.' + name + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListRemainder#',
                append: (name: string) => name + ' % divisor.Magnitude, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListMultiplication#',
                append: (name: string) => name + ' * factor.Magnitude, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListDivision#',
                append: (name: string) => name + ' / divisor.Magnitude, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorARemainderScalarB#',
                append: (name: string) => 'a.' + name + ' % b.Magnitude, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorATimesScalarB#',
                append: (name: string) => 'a.' + name + ' * b.Magnitude, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListScalarATimesVectorB#',
                append: (name: string) => 'a.Magnitude * b.' + name + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorADividedByScalarB#',
                append: (name: string) => 'a.' + name + ' / b.Magnitude, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListRemainderDouble#',
                append: (name: string) => name + ' % divisor, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListMultiplicationDouble#',
                append: (name: string) => name + ' * factor, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListDivisionDouble#',
                append: (name: string) => name + ' / divisor, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorARemainderDoubleB#',
                append: (name: string) => 'a.' + name + ' % b, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorATimesDoubleB#',
                append: (name: string) => 'a.' + name + ' * b, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListDoubleATimesVectorB#',
                append: (name: string) => 'a * b.' + name + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListVectorADividedByDoubleB#',
                append: (name: string) => 'a.' + name + ' / b, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListUnnamedDoubles#',
                append: (name: string) => 'double, ',
                slice: (result: string) => result.slice(0, -2)
            }
        ]

        for (let componentList of componentLists) {
            let listText: string = ''
            
            for (let componentName of getVectorComponentNames(dimensionality)) {
                listText += componentList.append(componentName)
            }

            text = text.replace(new RegExp(componentList.replace, 'g'), componentList.slice(listText))
        }

        return text
    }

    private setConditionalBlocks(vector: VectorQuantity, dimensionality: number, text: string): string {
        if (vector.component) {
            text = text.replace(/(?:\n|\r\n|\r)#ScalarQuantityComponent#/g, '')
            text = text.replace(/(?:\n|\r\n|\r)#\/ScalarQuantityComponent#/g, '')

            text = text.replace(/(?:\n|\r\n|\r)#NonScalarQuantityComponent#([^]+?)(?:\n|\r\n|\r)#\/NonScalarQuantityComponent#/g, '')
        } else {
            text = text.replace(/(?:\n|\r\n|\r)#NonScalarQuantityComponent#/g, '')
            text = text.replace(/(?:\n|\r\n|\r)#\/NonScalarQuantityComponent#/g, '')

            text = text.replace(/(?:\n|\r\n|\r)#ScalarQuantityComponent#([^]+?)(?:\n|\r\n|\r)#\/ScalarQuantityComponent#/g, '')
        }

        const emptyArrayOrFalse = (obj: string | string[] | false) => {
            return (obj === false || Array.isArray(obj) && obj.length === 0)
        }

        if (vector.component && !emptyArrayOrFalse(this.definitionReader.definitions.scalars[vector.component].square)) {
            text = text.replace(/(?:\n|\r\n|\r)#SquaredScalarQuantityComponent#/g, '')
            text = text.replace(/(?:\n|\r\n|\r)#\/SquaredScalarQuantityComponent#/g, '')
    
            text = text.replace(/(?:\n|\r\n|\r)#NonSquaredScalarQuantityComponent#([^]+?)(?:\n|\r\n|\r)#\/NonSquaredScalarQuantityComponent#/g, '')
        } else {
            text = text.replace(/(?:\n|\r\n|\r)#NonSquaredScalarQuantityComponent#/g, '')
            text = text.replace(/(?:\n|\r\n|\r)#\/NonSquaredScalarQuantityComponent#/g, '')
    
            text = text.replace(/(?:\n|\r\n|\r)#SquaredScalarQuantityComponent#([^]+?)(?:\n|\r\n|\r)#\/SquaredScalarQuantityComponent#/g, '')
        }

        if (dimensionality === 3) {
            text = text.replace(/(?:\n|\r\n|\r)#Vector3#/g, '')
            text = text.replace(/(?:\n|\r\n|\r)#\/Vector3#/g, '')
        } else {
            text = text.replace(/(?:\n|\r\n|\r)#Vector3#([^]+?)(?:\n|\r\n|\r)#\/Vector3#/g, '')
        }
        
        return text
    }

    private composeInterfacesText(vector: VectorQuantity): string {
        const interfaceTexts: string[] = []

        interfaceTexts.push('IMultiplicableVector#Dimensionality#Quantity<#Quantity#, Scalar>')
        interfaceTexts.push('IMultiplicableVector#Dimensionality#Quantity<Unhandled3, Unhandled>')

        interfaceTexts.push('IDivisibleVector#Dimensionality#Quantity<#Quantity#, Scalar>')
        interfaceTexts.push('IDivisibleVector#Dimensionality#Quantity<Unhandled3, Unhandled>')

        if (vector.component) {
            interfaceTexts.push('IDotableVector#Dimensionality#Quantity<#Component#, Vector3>')
        }
        interfaceTexts.push('IDotableVector#Dimensionality#Quantity<Unhandled, Unhandled#Dimensionality#>')

        interfaceTexts.push('ICrossableVector#Dimensionality#Quantity<#Quantity#, Vector#Dimensionality#>')
        interfaceTexts.push('ICrossableVector#Dimensionality#Quantity<Unhandled#Dimensionality#, Unhandled#Dimensionality#>')

        interfaceTexts.push('IGenericallyMultiplicableVector#Dimensionality#Quantity')
        interfaceTexts.push('IGenericallyDivisibleVector#Dimensionality#Quantity')
        interfaceTexts.push('IGenericallyDotableVector#Dimensionality#Quantity')
        interfaceTexts.push('IGenericallyCrossableVector#Dimensionality#Quantity')

        let interfacesText: string = ""
        for (let interfaceText of interfaceTexts) {
            interfacesText += '\t' + interfaceText + ',\n'
        }

        return interfacesText.slice(0, -2)
    }

    private composeConvertibleText(vector: VectorQuantity): string {
        let convertibleText: string = ''

        if (Array.isArray(vector.convertible)) {
            for (let convertible of vector.convertible) {
                for (let dimensionality of vector.dimensionalities) {
                    if (this.definitionReader.definitions.vectors[convertible].dimensionalities.includes(dimensionality)) {
                        convertibleText += '\t#Document:AsShared(#Quantity#, #Dimensionality#, ' + convertible + ')#\n'
                        convertibleText += '\tpublic ' + convertible + '#Dimensionality# As' + convertible + '#Dimensionality#() => new('
                        for (let name of getVectorComponentNames(dimensionality)) {
                            convertibleText += name + ', '
                        }
                    }
                    convertibleText = convertibleText.slice(0, -2) + ');\n'
                }
            }
        }

        return convertibleText.slice(0, -1)
    }

    private composeQuantityToUnitText(vector: VectorQuantity): string {
        if (vector.unitBias) {
            return '(#quantity#.ToVector#Dimensionality#() / #UnitVariable#.BaseScale - Vector#Dimensionality#.Ones * #UnitVariable#.Bias) / #UnitVariable#.Prefix.Scale'
        } else {
            return '#quantity#.ToVector#Dimensionality#() / #UnitVariable#.Factor'
        }
    }

    private composeUnitsText(vector: VectorQuantity): string {
        let unitsText: string = ""

        if (Array.isArray(vector.units)) {
            for (let unit of vector.units) {
                if (unit.special && unit.separator === true) {
                    unitsText += '\n'
                } else if (!unit.special) {
                    unitsText += '\t#Document:InUnit(quantity = #Quantity#, dimensionality = #Dimensionality#, unit = #Unit#, unitName = ' + unit.singular + ')#\n'
                    unitsText += '\tpublic Vector#Dimensionality# ' + unit.plural + ' => '
                    unitsText += 'InUnit(' + vector.unit + '.' + unit.singular + ');\n'
                }
            }
        }

        return unitsText
    }

    private reportErrorMissingEntries(vector: VectorQuantity, entries: string[]): void {
        console.error('Vector quantity: [' + vector.name + '] is missing ' + (entries.length > 1 ? 'entries' : 'entry') + ': ' + entries + '.')
    }

    private reportErrorDocumentationFileNotFound(vector: VectorQuantity, fileName: string): void {
        console.error('Could not locate documentation file for vector quantity: [' + vector.name + '], tried: [' + fileName + '].')
    }

    private reportWarningRedundantEntries(vector: VectorQuantity, entries: string[]): void {
        console.warn('Vector quantity: [' + vector.name + '] has redundant ' + (entries.length > 1 ? 'entries' : 'entry') + ':' + entries + '.')
    }
}