import fsp from 'fs/promises'
import path from 'path'
import { DefinitionReader } from './DefinitionReader'
import { TemplateReader } from './TemplateReader'
import { ScalarQuantity } from './ScalarQuantity'
import { VectorQuantity } from './VectorQuantity'
import { Unit } from './Unit'
import { fixLines, getConstants, getConvertible, getUnit, getUnits, getUnitQuantity, getVectorComponent, getVectorComponentNames, lowerCase, parseUnitPlural } from './Utility'

export class VectorTestsGenerator {

    public constructor(private destination: string, private definitionReader: DefinitionReader, private templateReader: TemplateReader, private destroy: boolean) {}

    public async generate(): Promise<void> {
        const keys: string[] = Object.keys(this.definitionReader.definitions.vectors)

        await Promise.all(keys.map(async (key: string) => {
            const vector: VectorQuantity = this.definitionReader.definitions.vectors[key]
            for (let dimensionality of vector.dimensionalities) {
                this.generateDataset(vector, dimensionality)
                this.generateCastTests(vector, dimensionality)
                this.generateConstructorTests(vector, dimensionality)
                this.generateConvertibleTests(vector, dimensionality)
                this.generateDotTests(vector, dimensionality)
                this.generateInUnitsTests(vector, dimensionality)
                this.generateMagnitudeTests(vector, dimensionality)
                this.generateMathOperationsTests(vector, dimensionality)
                this.generateNormalizeTests(vector, dimensionality)

                if (dimensionality === 3) {
                    this.generateCrossTests(vector, dimensionality)
                    this.generateTransformTests(vector, dimensionality)
                }
            }
        }))
    }

    private async generateDataset(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.dataset

        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Datasets\\Generated\\' + vector.name + dimensionality + 'Dataset.g.cs', text)
    }

    private async generateCastTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.cast

        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\CastTests.g.cs', text)
    }
    
    private async generateConstructorTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.constructor

        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\ConstructorTests.g.cs', text)
    }

    private async generateConvertibleTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.convertible

        const convertibleText: string = this.composeConvertibleText(vector, dimensionality)
        text = text.replace(/#Convertible#/g, convertibleText)

        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\ConvertibleTests.g.cs', text)
    }

    private async generateCrossTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.cross

        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\CrossTests.g.cs', text)
    }

    private async generateDotTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.dot

        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\DotTests.g.cs', text)
    }

    private async generateInUnitsTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.inUnits

        const inUnitsText: string = this.composeInUnitsText(vector, dimensionality)
        text = text.replace(/#Units#/g, inUnitsText)

        const constantMultiplesText: string = this.composeConstantMultiplesText(vector, dimensionality)
        text = text.replace(/#Constants#/g, constantMultiplesText)

        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\InUnitsTests.g.cs', text)
    }

    private async generateMagnitudeTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.magnitude

        text = this.setConditionalBlocks(vector, text)
        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\MagnitudeTests.g.cs', text)
    }

    private async generateMathOperationsTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.mathOperations

        text = this.setConditionalBlocks(vector, text)
        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\MathOperationsTests.g.cs', text)
    }

    private async generateNormalizeTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.normalize

        text = this.setConditionalBlocks(vector, text)
        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\NormalizeTests.g.cs', text)
    }

    private async generateTransformTests(vector: VectorQuantity, dimensionality: number): Promise<void> {
        let text: string = this.templateReader.vectorTests.transform

        text = this.setConditionalBlocks(vector, text)
        text = this.setComponentListTexts(vector, dimensionality, text)
        text = this.insertNames(text, vector, dimensionality)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + vector.name + dimensionality + 'Tests\\Generated\\TransformTests.g.cs', text)
    }

    private composeInUnitsText(vector: VectorQuantity, dimensionality: number): string {
        let unitsText: string = ''
        
        const units: Unit['units'] = getUnits(this.definitionReader.definitions, vector)
        for (let unit of units) {
            if (!unit.special) {
                unitsText += '\t[Theory]\n'
                unitsText += '\t[ClassData(typeof(Vector#Dimensionality#Dataset))]\n'
                unitsText += '\tpublic void In' + unit.name + '(Vector#Dimensionality# expected)\n'
                unitsText += '\t{\n'
                unitsText += '\t\t#Quantity##Dimensionality# quantity = new(expected, #Unit#.' + unit.name + ');\n\n'
                unitsText += '\t\tVector3 actual = quantity.' + parseUnitPlural(unit.name, unit.plural) + ';\n\n'
                unitsText += '\t\tUtility.AssertExtra.AssertEqualComponents(expected, actual);\n'
                unitsText += '\t}\n\n'
            }
        }

        return unitsText.slice(0, -2)
    }

    private composeConstantMultiplesText(vector: VectorQuantity, dimensionality: number): string {
        let multiplesText: string = ''
        
        const constants: Unit['constants'] = getConstants(this.definitionReader.definitions, vector)
        for (let constant of constants) {
            if (!constant.special) {
                multiplesText += '\t[Theory]\n'
                multiplesText += '\t[ClassData(typeof(Vector#Dimensionality#Dataset))]\n'
                multiplesText += '\tpublic void ' + constant.name + 'Multiples(Vector#Dimensionality# expected)\n'
                multiplesText += '\t{\n'
                multiplesText += '\t\t#Quantity##Dimensionality# quantity = new(expected, #Unit#.' + constant.name + ');\n\n'
                multiplesText += '\t\tVector3 actual = quantity.' + constant.name + 'Multiples;\n\n'
                multiplesText += '\t\tUtility.AssertExtra.AssertEqualComponents(expected, actual);\n'
                multiplesText += '\t}\n\n'
            }
        }

        return multiplesText.slice(0, -2)
    }

    private composeConvertibleText(vector: VectorQuantity, dimensionality: number): string {
        let convertibleText: string = ''

        for (let convertible of getConvertible(this.definitionReader.definitions, vector)) {
            convertibleText += '\t[Theory]\n'
            convertibleText += '\t[ClassData(typeof(#Quantity##Dimensionality#Dataset))]\n'
            convertibleText += '\tpublic void ' + convertible.name + '(#Quantity##Dimensionality# quantity)\n'
            convertibleText += '\t{\n'
            convertibleText += '\t\t' + convertible.name + '#Dimensionality# result = quantity.As' + convertible.name + ';\n\n'
            for (let name of getVectorComponentNames(dimensionality)) {
                convertibleText += '\t\tAssert.Equal(quantity.' + name + ', result.' + name + ', 2);\n'
            }
            convertibleText += '\t}\n\n'
        }

        return convertibleText.slice(0, -2)
    }

    private insertNames(text: string, vector: VectorQuantity, dimensionality: number): string {
        const unit: Unit = getUnit(this.definitionReader.definitions, vector)
        text = text.replace(/#Unit#/g, 'UnitOf' + unit.name)

        text = text.replace(/#UnitQuantity#/g, getUnitQuantity(this.definitionReader.definitions, unit).name)
        text = text.replace(/#UnbiasedQuantity#/g, unit.unbiasedQuantity ? unit.unbiasedQuantity : 'NoUnbiasedQuantityError')

        text = text.replace(/#Quantity#/g, vector.name)
        
        const component: ScalarQuantity = getVectorComponent(this.definitionReader.definitions, vector)
        text = text.replace(/#Component#/g, component.name)
        if (component.square !== undefined && component.square.length > 0) {
            text = text.replace(/#SquaredComponent#/g, component.square[0])
        }

        text = text.replace(/#Dimensionality#/g, dimensionality.toString())

        return text
    }

    private setConditionalBlocks(vector: VectorQuantity, text: string): string {
        if (vector.unitBias) {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)Biased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#BiasedUnit#([^]+?)#\/BiasedUnit#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Unbiased#([^]+?)#\/Unbiased#/g, '')
        } else if (getUnit(this.definitionReader.definitions, vector).bias) {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)BiasedUnit#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Biased#([^]+?)#\/Biased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Unbiased#([^]+?)#\/Unbiased#/g, '')
        } else {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)Unbiased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Biased#([^]+?)#\/Biased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#BiasedUnit#([^]+?)#\/BiasedUnit#/g, '')
        }

        const componentSquare: string[] | undefined = getVectorComponent(this.definitionReader.definitions, vector).square
        if (Array.isArray(componentSquare) && componentSquare.length > 0) {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)HasSquaredComponent#/g, '')
        } else {
            text = text.replace(/(\n|\r\n|\r?)#HasSquaredComponent#([^]+?)#\/HasSquaredComponent#/g, '')
        }
        
        return text
    }

    private setComponentListTexts(vector: VectorQuantity, dimensionality: number, text: string): string {
        const vectorComponent: ScalarQuantity = getVectorComponent(this.definitionReader.definitions, vector)

        const componentLists: { replace: string, append: (name: string) => string, slice: (text: string) => string }[] = [
            {
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
            }, {
                replace: '#ComponentListDoubleDatasets#',
                append: (name: string) => 'DoubleDataset, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListComponentDatasets#',
                append: (name: string) => vectorComponent.name + 'Dataset, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListScalarDatasets#',
                append: (name: string) => 'ScalarDataset, ',
                slice: (result: string) => result.slice(0, -2)
            }, {
                replace: '#ComponentListSquareSum#',
                append: (name: string) => 'quantity.' + name + ' * quantity.' + name + ' + ',
                slice: (result: string) => result.slice(0, -3)
            }, {
                replace: '#AssertScalarsUnit#',
                append: (name: string) => '\t\tAssert.Equal(' + lowerCase(name) + ' * unit.#UnitQuantity#.Magnitude, quantity.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertVector3Unit#',
                append: (name: string) => '\t\tAssert.Equal(vector.' + name + ' * unit.#UnitQuantity#.Magnitude, quantity.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertDoublesUnit#',
                append: (name: string) => '\t\tAssert.Equal(' + lowerCase(name) + ' * unit.#UnitQuantity#.Magnitude, quantity.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertMultipliedMagnitude#',
                append: (name: string) => '\t\tAssert.Equal(quantity.' + name + ' * factor.Magnitude, result.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertMultipliedMagnitudeLHS#',
                append: (name: string) => '\t\tAssert.Equal(quantity.' + name + ' * factor.Magnitude, resultLHS.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertMultipliedMagnitudeRHS#',
                append: (name: string) => '\t\tAssert.Equal(factor.Magnitude * quantity.' + name + ', resultRHS.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertDividedMagnitude#',
                append: (name: string) => '\t\tAssert.Equal(quantity.' + name + ' / divisor.Magnitude, result.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertRemainder#',
                append: (name: string) => '\t\tAssert.Equal(quantity.' + name + ' % divisor, result.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertMultiplied#',
                append: (name: string) => '\t\tAssert.Equal(quantity.' + name + ' * factor, result.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertMultipliedLHS#',
                append: (name: string) => '\t\tAssert.Equal(quantity.' + name + ' * factor, resultLHS.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertMultipliedRHS#',
                append: (name: string) => '\t\tAssert.Equal(factor * quantity.' + name + ', resultRHS.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertDivided#',
                append: (name: string) => '\t\tAssert.Equal(quantity.' + name + ' / divisor, result.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            }, {
                replace: '#AssertToUnit#',
                append: (name: string) => '\t\tAssert.Equal(quantity.' + name + ' / unit.#UnitQuantity#.Magnitude, components.' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
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

    private async attemptWriteFile(destination: string, text: string): Promise<void> {
        if (await fsp.stat(destination).then(() => true, () => false)) {
            if (!this.destroy) {
                const items = await fsp.readdir(destination)
                if (items.length > 0) {
                    console.log('The directory [' + destination + '] is not empty. To continue and potentially LOSE EXISTING CONTENT, add the flag \'-DESTROY\'')
                    return
                }
            }
        }

        await fsp.mkdir(path.dirname(destination), { recursive: true })
        await fsp.writeFile(destination, text)
    }
}