import fsp from 'fs/promises'
import { Documenter } from './Documenter'
import { DefinitionReader } from './DefinitionReader'
import { TemplateReader } from './TemplateReader'
import { ScalarQuantity } from './ScalarQuantity'
import { VectorQuantity } from './VectorQuantity'
import { Unit } from './Unit'
import { composeUnitsNameList, composeBasesNameList, fixLines, getConstants, getConvertible, getDefaultConstant, getDefaultUnit,
    getUnit, getUnits, getUnitQuantity, getVectorComponent, getVectorComponentNames, lowerCase, parseUnitPlural } from './Utility'

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

        const constantMultiplesText: string = this.composeConstantMultiplesText(vector)
        text = text.replace(/#ConstantMultiples#/g, constantMultiplesText)

        text = this.insertNames(text, vector, dimensionality)

        if (await fsp.stat(this.documentationDirectory + '\\Vectors\\' + vector.name + dimensionality + '.txt').catch(() => false)) {
            text = await Documenter.document(text, this.documentationDirectory + '\\Vectors\\' + vector.name + dimensionality + '.txt')
        } else if (await fsp.stat(this.documentationDirectory + '\\Vectors\\' + vector.name + '.txt').catch(() => false)) {
            text = await Documenter.document(text, this.documentationDirectory + '\\Vectors\\' + vector.name + '.txt')
        } else {
            this.reportErrorDocumentationFileNotFound(vector, this.documentationDirectory + '\\Vectors\\' + vector.name + '.txt')
        }

        text = fixLines(text)

        await fsp.writeFile(this.destination + '\\' + vector.name + dimensionality + '.g.cs', text)
    }

    private insertNames(text: string, vector: VectorQuantity, dimensionality: number): string {
        const unit: Unit = getUnit(this.definitionReader.definitions, vector)
        text = text.replace(/#Unit#/g, 'UnitOf' + unit.name)
        text = text.replace(/#UnitVariable#/g, lowerCase('UnitOf' + unit.name))

        text = text.replace(/#UnitQuantity#/g, getUnitQuantity(this.definitionReader.definitions, unit).name)

        text = this.insertDefaultUnitNames(text, vector)

        const component: ScalarQuantity = getVectorComponent(this.definitionReader.definitions, vector)
        text = text.replace(/#Component#/g, component.name)

        if (component.square !== undefined && component.square.length > 0) {
            text = text.replace(/#SquaredComponent#/g, component.square[0])
        }

        text = text.replace(/#SingularUnits#/g, composeUnitsNameList(this.definitionReader.definitions, vector).singular)
        text = text.replace(/#PluralUnits#/g, composeUnitsNameList(this.definitionReader.definitions, vector).plural)
        text = text.replace(/#SingularBases#/g, composeBasesNameList(this.definitionReader.definitions, vector).singular)
        text = text.replace(/#PluralBases#/g, composeBasesNameList(this.definitionReader.definitions, vector).plural)

        text = text.replace(/#Quantity#/g, vector.name + dimensionality)
        text = text.replace(/#quantity#/g, lowerCase(vector.name + dimensionality))
        text = text.replace(/#Dimensionality#/g, dimensionality.toString())
        return text
    }

    private insertDefaultUnitNames(text: string, vector: VectorQuantity): string {
        let defaultUnit: Unit['units'][number] | NonNullable<Unit['constants']>[number] | undefined = getDefaultUnit(this.definitionReader.definitions, vector)
        let isConstant: boolean = false

        if (defaultUnit === undefined) {
            defaultUnit = getDefaultConstant(this.definitionReader.definitions, vector)
            isConstant = true
        }

        if (defaultUnit !== undefined && !defaultUnit.special) {
            text = text.replace(/#DefaultUnit#/g, defaultUnit.name)
            text = text.replace(/#DefaultUnits#/g, parseUnitPlural(defaultUnit.name, defaultUnit.plural) + (isConstant ? 'Multiples' : ''))
            if (defaultUnit.symbol === undefined) {
                this.reportErrorMissingDefaultUnitSymbol(vector)
                text = text.replace(/#DefaultSymbol#/g, 'NoDefaultUnitSymbolError')
            } else {
                text = text.replace(/#DefaultSymbol#/g, defaultUnit.symbol)
            }
        } else {
            this.reportErrorMissingDefaultUnit(vector)
            text = text.replace(/#DefaultUnit#/g, 'NoDefaultUnitError')
            text = text.replace(/#DefaultUnits#/g, 'NoDefaultUnitError')
            text = text.replace(/#DefaultSymbol#/g, 'NoDefaultUnitError')
        }

        return text
    }

    private setComponentListTexts(vector: VectorQuantity, dimensionality: number, text: string): string {
        const vectorComponent: ScalarQuantity = getVectorComponent(this.definitionReader.definitions, vector)

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
                append: (name: string) => vectorComponent.name + ' ' + lowerCase(name) + ', ',
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
                replace: '#ComponentListLowerCaseName#',
                append: (name: string) => lowerCase(name) + ', ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListLowerCaseMagnitudes#',
                append: (name: string) => lowerCase(name) + '.Magnitude, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListMagnitudeFromUnit#',
                append: vector.unitBias ?
                    (name: string) => '(' + lowerCase(name) + ' * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale, ' :
                    (name: string) => {
                        return getUnitQuantity(this.definitionReader.definitions, getUnit(this.definitionReader.definitions, vector)).name == vector.name ?
                            lowerCase(name) + ' * #UnitVariable#.#UnitQuantity#, ' :
                            lowerCase(name) + ' * #UnitVariable#.#UnitQuantity#.Magnitude, '
                    },
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
            text = text.replace(/(\n|\r\n|\r?)#(\/?)ScalarQuantityComponent#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#NonScalarQuantityComponent#([^]+?)#\/NonScalarQuantityComponent#/g, '')
        } else {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)NonScalarQuantityComponent#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#ScalarQuantityComponent#([^]+?)#\/ScalarQuantityComponent#/g, '')
        }

        const component: ScalarQuantity = getVectorComponent(this.definitionReader.definitions, vector)
        if (component.square !== undefined && component.square.length > 0) {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)SquaredScalarQuantityComponent#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#NonSquaredScalarQuantityComponent#([^]+?)#\/NonSquaredScalarQuantityComponent#/g, '')
        } else {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)NonSquaredScalarQuantityComponent#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#SquaredScalarQuantityComponent#([^]+?)#\/SquaredScalarQuantityComponent#/g, '')
        }

        if (dimensionality === 3) {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)Vector3#/g, '')
        } else {
            text = text.replace(/(\n|\r\n|\r?)#Vector3#([^]+?)#\/Vector3#/g, '')
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

        for (let convertible of getConvertible(this.definitionReader.definitions, vector)) {
            if (convertible.type === 'Vector') {
                for (let dimensionality of vector.dimensionalities) {
                    if (convertible.dimensionalities.includes(dimensionality)) {
                        convertibleText += '\t#Document:AsShared(#Quantity#, #Dimensionality#, ' + convertible.name + ')#\n'
                        convertibleText += '\tpublic ' + convertible.name + '#Dimensionality# As' + convertible.name + '#Dimensionality#() => new('
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
        if (vector.unitBias === true) {
            return '(#quantity#.ToVector#Dimensionality#() + Vector#Dimensionality#.Ones * #UnitVariable#.Offset) / #UnitVariable#.#UnbiasedQuantity#.Magnitude'
        } else if (getUnit(this.definitionReader.definitions, vector).bias) {
            return '#quantity#.ToVector#Dimensionality#() / #UnitVariable#.#UnbiasedQuantity#.Magnitude'
        } else {
            return '#quantity#.ToVector#Dimensionality#() / #UnitVariable#.#UnitQuantity#.Magnitude'
        }
    }

    private composeUnitsText(vector: VectorQuantity): string {
        const units: Unit['units'] | undefined = getUnits(this.definitionReader.definitions, vector)

        if (units === undefined) {
            return ''
        }

        let unitsText: string = ''

        for (let unit of units) {
            if (unit.special && unit.separator === true) {
                unitsText += '\n'
            } else if (!unit.special) {
                unitsText += '\t#Document:InUnit(quantity = #Quantity#, dimensionality = #Dimensionality#, unit = #Unit#, unitName = ' + unit.name + ')#\n'
                unitsText += '\tpublic Vector#Dimensionality# ' + parseUnitPlural(unit.name, unit.plural) + ' => '
                unitsText += 'InUnit(#Unit#.' + unit.name + ');\n'
            }
        }

        return unitsText
    }

    private composeConstantMultiplesText(vector: VectorQuantity): string {
        let unitsText: string = ''

        const constants: Unit['constants'] = getConstants(this.definitionReader.definitions, vector)
        if (constants !== undefined)
        {
            for (let constant of constants) {
                if (constant.special) {
                    if (constant.separator) {
                        unitsText += '\n'
                    }
                } else {
                    unitsText += '\t#Document:InConstant(#Quantity#, #Unit#, ' + constant.name + ')#\n'
                    unitsText += '\tpublic Vector#Dimensionality# ' + parseUnitPlural(constant.name, constant.plural) + 'Multiples => InUnit(#Unit#.' + constant.name + ');\n'
                }
            }
        }

        return unitsText
    }

    private reportErrorDocumentationFileNotFound(vector: VectorQuantity, fileName: string): void {
        console.error('Could not locate documentation file for vector quantity: [' + vector.name + '], tried: [' + fileName + '].')
    }

    private reportErrorMissingDefaultUnitSymbol(vector: VectorQuantity): void {
        const defaultUnit: Unit['units'][number] | NonNullable<Unit['constants']>[number] | undefined = getDefaultUnit(this.definitionReader.definitions, vector)
        if (defaultUnit === undefined) {
            getDefaultConstant(this.definitionReader.definitions, vector)
        }

        if (defaultUnit === undefined || defaultUnit.special) {
            console.error('Default unit of vector quantity: [' + vector.name + '] is missing symbol.')
        } else {
            console.error('Default unit: [' + defaultUnit.name + '] of vector quantity: [' + vector.name + '] is missing symbol.')
        }
    }

    private reportErrorMissingDefaultUnit(vector: VectorQuantity): void {
        console.error('Could not identify default unit of vector quantity: [' + vector.name + '].')
    }
}