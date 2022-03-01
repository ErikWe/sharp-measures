import fsp from 'fs/promises'
import path from 'path'
import { DefinitionReader } from './DefinitionReader'
import { TemplateReader } from './TemplateReader'
import { ScalarQuantity } from './ScalarQuantity'
import { VectorQuantity } from './VectorQuantity'
import { Unit } from './Unit'
import { fixLines, getBases, getConstants, getConvertible, getUnit, getUnits, getUnitQuantity, getVectorComponentNames,
    getVectorVersionOfScalar, lowerCase, parseUnitPlural } from './Utility'

export class ScalarTestsGenerator {

    public constructor(private destination: string, private definitionReader: DefinitionReader, private templateReader: TemplateReader, private destroy: boolean) {}

    public async generate(): Promise<void> {
        const keys: string[] = Object.keys(this.definitionReader.definitions.scalars)

        await Promise.all(keys.map(async (key: string) => {
            const scalar: ScalarQuantity = this.definitionReader.definitions.scalars[key]
            this.generateDataset(scalar)
            this.generateCastTests(scalar)
            this.generateComparisonTests(scalar)
            this.generateConstructorTests(scalar)
            this.generateConvertibleTests(scalar)
            this.generateEqualityTests(scalar)
            this.generateInUnitsTests(scalar)
            this.generateMathFunctionsTests(scalar)
            this.generateMathOperationsTests(scalar)
            this.generateMathPowersTests(scalar)
            this.generatePropertiesTests(scalar)
            this.generateToVectorTests(scalar)
        }))
    }

    private async generateDataset(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.dataset

        text = this.insertNames(text, scalar)
        text = fixLines(text)
        await this.attemptWriteFile(this.destination + '\\Datasets\\Generated\\' + scalar.name + 'Dataset.g.cs', text)
    }

    private async generateCastTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.cast

        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\CastTests.g.cs', text)
    }

    private async generateComparisonTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.comparison

        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\ComparisonTests.g.cs', text)
    }

    private async generateConstructorTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.constructor

        text = this.setConditionalBlocks(scalar, text)

        const constantsText: string = this.composeConstructorConstantsText(scalar)
        text = text.replace(/#Constants#/g, constantsText)

        const basesText: string = this.composeConstructorBasesText(scalar)
        text = text.replace(/#Bases#/g, basesText)

        const fromPowers: { text: string, usingSystem: boolean } = this.composeConstructorFromPowersText(scalar)
        text = text.replace(/#FromPowers#/g, fromPowers.text)

        if (fromPowers.usingSystem) {
            text = text.replace(/#UsingSystem#/g, 'using System;')
        } else {
            text = text.replace(/(\n|\r\n|\r)#UsingSystem#(\n|\r\n|\r)/g, '')
        }

        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\ConstructorTests.g.cs', text)
    }

    private async generateConvertibleTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.convertible

        const convertibleText: string = this.composeConvertibleText(scalar)
        text = text.replace(/#Convertible#/g, convertibleText)

        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\ConvertibleTests.g.cs', text)
    }

    private async generateEqualityTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.equality

        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\EqualityTests.g.cs', text)
    }

    private async generateInUnitsTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.inUnits
        
        const inUnitsText: string = this.composeInUnitsText(scalar)
        text = text.replace(/#Units#/g, inUnitsText)
        
        const constantMultiplesText: string = this.composeConstantMultiplesText(scalar)
        text = text.replace(/#Constants#/g, constantMultiplesText)

        text = this.setConditionalBlocks(scalar, text)
        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\InUnitsTests.g.cs', text)
    }

    private async generateMathFunctionsTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.mathFunctions

        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\MathFunctionsTests.g.cs', text)
    }

    private async generateMathOperationsTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.mathOperations

        text = this.setConditionalBlocks(scalar, text)
        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\MathOperationsTests.g.cs', text)
    }

    private async generateMathPowersTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.mathPowers

        const powers: { powerName: string, data: string[] | undefined }[] = [
            { powerName: 'Invert', data: scalar.inverse },
            { powerName: 'Square', data: scalar.square },
            { powerName: 'Cube', data: scalar.cube },
            { powerName: 'SquareRoot', data: scalar.squareRoot },
            { powerName: 'CubeRoot', data: scalar.cubeRoot }
        ]

        let anyPower: boolean = false

        for (let power of powers) {
            if (power.data && power.data.length > 0) {
                text = text.replace(new RegExp('(\\n|\\r\\n|\\r?)#(\\/?)' + power.powerName + '#', 'g'), '')
                anyPower = true
            } else {
                text = text.replace(new RegExp('(\\n|\\r\\n|\\}r?)#' + power.powerName + '#([^]+?)#\\/' + power.powerName + '#', 'g'), '')
            }
        }

        if (!anyPower) {
            return
        }

        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\MathPowersTests.g.cs', text)
    }

    private async generatePropertiesTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.properties

        text = this.insertNames(text, scalar)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\PropertiesTests.g.cs', text)
    }

    private async generateToVectorTests(scalar: ScalarQuantity): Promise<void> {
        const vector: VectorQuantity | undefined = getVectorVersionOfScalar(this.definitionReader.definitions, scalar)

        if (vector === undefined) {
            return
        }

        const componentLists: { replace: string, append: (name: string) => string, slice: (text: string) => string }[] = [
            {
                replace: '#AssertEqualVectorMethod#',
                append: (name: string) => '\t\tAssert.Equal(quantity.Magnitude * vector.Magnitude' + name +', result.Magnitude' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            },
            {
                replace: '#DoubleDatasets#',
                append: (name: string) => 'DoubleDataset, ',
                slice: (result: string) => result.slice(0, -2)
            },
            {
                replace: '#ScalarDatasets#',
                append: (name: string) => 'ScalarDataset, ',
                slice: (result: string) => result.slice(0, -2)
            },
            {
                replace: '#Doubles#',
                append: (name: string) => 'double ' + lowerCase(name) + ', ',
                slice: (result: string) => result.slice(0, -2)
            },
            {
                replace: '#Scalars#',
                append: (name: string) => 'Scalar ' + lowerCase(name) + ', ',
                slice: (result: string) => result.slice(0, -2)
            },
            {
                replace: '#Tuple#',
                append: (name: string) => lowerCase(name) + ', ',
                slice: (result: string) => result.slice(0, -2)
            },
            {
                replace: '#AssertEqualTupleMethod#',
                append: (name: string) => '\t\tAssert.Equal(quantity.Magnitude * ' + lowerCase(name) + ', result.Magnitude' + name + ', 2);\n',
                slice: (result: string) => result.slice(0, -1)
            },
            {
                replace: '#AssertEqualVectorOperator#',
                append: (name: string) => '\t\tAssert.Equal(quantity.Magnitude * vector.Magnitude' + name + ', resultLHS.Magnitude' + name + ', 2);\n' +
                    '\t\tAssert.Equal(vector.Magnitude' + name + ' * quantity.Magnitude, resultRHS.Magnitude' + name + ', 2);\n\n',
                slice: (result: string) => result.slice(0, -2)
            },
            {
                replace: '#AssertEqualTupleOperator#',
                append: (name: string) => '\t\tAssert.Equal(quantity.Magnitude * ' + lowerCase(name) + ', resultLHS.Magnitude' + name + ', 2);\n' +
                    '\t\tAssert.Equal(' + lowerCase(name) + ' * quantity.Magnitude, resultRHS.Magnitude' + name + ', 2);\n\n',
                slice: (result: string) => result.slice(0, -2)
            }
        ]

        for (let dimensionality of vector.dimensionalities) {
            let text: string = this.templateReader.scalarTests.toVectorN

            text = text.replace(/#Dimensionality#/g, dimensionality.toString())


            for (let componentList of componentLists) {
                let listText: string = ''
                
                for (let componentName of getVectorComponentNames(dimensionality)) {
                    listText += componentList.append(componentName)
                }
    
                text = text.replace(new RegExp(componentList.replace, 'g'), componentList.slice(listText))
            }

            text = this.insertNames(text, scalar)
            text = fixLines(text)
            this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\ToVector' + dimensionality + 'Tests.g.cs', text)
        }
    }

    private composeConstructorConstantsText(scalar: ScalarQuantity): string {
        const unit: Unit = getUnit(this.definitionReader.definitions, scalar)
        let constantsText: string = ''

        for (let constant of getConstants(this.definitionReader.definitions, scalar)) {
            if (!constant.special) {
                constantsText += '\n'
                constantsText += '\t[Fact]\n'
                constantsText += '\tpublic void ' + constant.name + '_ShouldMatchUnitScale()\n'
                constantsText += '\t{\n'
                constantsText += '\t\t#Quantity# quantity = #Quantity#.' + constant.name + ';\n'
                constantsText += '\n'
                
                if (unit.bias) {
                    constantsText += '\t\tAssert.Equal(#Unit#.' + constant.name + '.#UnbiasedQuantity#.Magnitude, quantity.Magnitude, 2);\n'
                } else {
                    constantsText += '\t\tAssert.Equal(#Unit#.' + constant.name + '.#UnitQuantity#.Magnitude, quantity.Magnitude, 2);\n'
                }

                constantsText += '\t}\n'
            }
        }
        
        return constantsText.slice(1, -1)
    }

    private composeConstructorBasesText(scalar: ScalarQuantity): string {
        const unit: Unit = getUnit(this.definitionReader.definitions, scalar)
        let basesText: string = ''

        for (let base of getBases(this.definitionReader.definitions, scalar)) {
            if (!base.special) {
                basesText += '\n'
                basesText += '\t[Fact]\n'
                basesText += '\tpublic void One' + base.name + '_ShouldMatchUnitScale()\n'
                basesText += '\t{\n'
                basesText += '\t\t#Quantity# quantity = #Quantity#.One' + base.name + ';\n'
                basesText += '\n'
                if (unit.bias) {
                    basesText += '\t\tAssert.Equal(#Unit#.' + base.name + '.#UnbiasedQuantity#.Magnitude, quantity.Magnitude, 2);\n'
                } else {
                    basesText += '\t\tAssert.Equal(#Unit#.' + base.name + '.#UnitQuantity#.Magnitude, quantity.Magnitude, 2);\n'
                }
                basesText += '\t}\n'
            }
        }
        
        return basesText.slice(1, -1)
    }

    private composeConstructorFromPowersText(scalar: ScalarQuantity): { text: string, usingSystem: boolean } {
        const powers: { data: string[] | undefined, expression: string, system: boolean }[] = [
            { data: scalar.inverse, expression: '1 / sourceQuantity.Magnitude', system: false },
            { data: scalar.square, expression: 'Math.Sqrt(sourceQuantity.Magnitude)', system: true },
            { data: scalar.cube, expression: 'Math.Cbrt(sourceQuantity.Magnitude)', system: true },
            { data: scalar.squareRoot, expression: 'Math.Pow(sourceQuantity.Magnitude, 2)', system: true },
            { data: scalar.cubeRoot, expression: 'Math.Pow(sourceQuantity.Magnitude, 3)', system: true }
        ]

        let fromText: string = ''
        let usingSystem: boolean = false

        for (let power of powers) {
            if (power.data !== undefined) {
                if (power.system) {
                    usingSystem = true
                }

                for (let source of power.data) {
                    fromText += '\t[Theory]\n'
                    fromText += '\t[ClassData(typeof(' + source + 'Dataset))]\n'
                    fromText += '\tpublic void From' + source + '_ShouldMatchExpression(' + source + ' sourceQuantity)\n'
                    fromText += '\t{\n'
                    fromText += '\t\t' + scalar.name + ' quantity = ' + scalar.name + '.From(sourceQuantity);\n\n'
                    fromText += '\t\tAssert.Equal(' + power.expression + ', quantity.Magnitude, 2);\n'
                    fromText += '\t}\n\n'
                }
            }
        }

        if (scalar.squareRoot !== undefined) {
            for (let source of scalar.squareRoot) {
                fromText += '\t[Theory]\n'
                fromText += '\t[ClassData(typeof(GenericDataset<' + source + 'Dataset, ' + source + 'Dataset>))]\n'
                fromText += '\tpublic void FromTwo' + source + '_ShouldMatchExpression(' + source + ' sourceQuantity1, ' + source + ' sourceQuantity2)\n'
                fromText += '\t{\n'
                fromText += '\t\t' + scalar.name + ' quantity = ' + scalar.name + '.From(sourceQuantity1, sourceQuantity2);\n\n'
                fromText += '\t\tAssert.Equal(sourceQuantity1.Magnitude * sourceQuantity2.Magnitude, quantity.Magnitude, 2);\n'
                fromText += '\t}\n\n'
            }
        }

        if (scalar.cubeRoot !== undefined) {
            for (let source of scalar.cubeRoot) {
                fromText += '\t[Theory]\n'
                fromText += '\t[ClassData(typeof(GenericDataset<' + source + 'Dataset, ' + source + 'Dataset, ' + source + 'Dataset>))]\n'
                fromText += '\tpublic void FromThree' + source + '_ShouldMatchExpression(' + source + ' sourceQuantity1, ' + source + ' sourceQuantity2, ' + source + ' sourceQuantity3)\n'
                fromText += '\t{\n'
                fromText += '\t\t' + scalar.name + ' quantity = ' + scalar.name + '.From(sourceQuantity1, sourceQuantity2, sourceQuantity3);\n\n'
                fromText += '\t\tAssert.Equal(sourceQuantity1.Magnitude * sourceQuantity2.Magnitude * sourceQuantity3.Magnitude, quantity.Magnitude, 2);\n'
                fromText += '\t}\n\n'
            }
        }

        return { text: fromText.slice(0, -2), usingSystem: usingSystem }
    }

    private composeConvertibleText(scalar: ScalarQuantity): string {
        let convertibleText: string = ''

        for (let convertible of getConvertible(this.definitionReader.definitions, scalar)) {
            convertibleText += '\t[Theory]\n'
            convertibleText += '\t[ClassData(typeof(#Quantity#Dataset))]\n'
            convertibleText += '\tpublic void ' + convertible.name + '(#Quantity# quantity)\n'
            convertibleText += '\t{\n'
            convertibleText += '\t\t' + convertible.name + ' result = quantity.As' + convertible.name + ';\n\n'
            convertibleText += '\t\tAssert.Equal(quantity.Magnitude, result.Magnitude, 2);\n'
            convertibleText += '\t}\n\n'
        }

        return convertibleText.slice(0, -2)
    }

    private composeInUnitsText(scalar: ScalarQuantity): string {
        let unitsText: string = ''
        
        const units: Unit['units'] = getUnits(this.definitionReader.definitions, scalar)
        for (let unit of units) {
            if (!unit.special) {
                unitsText += '\t[Theory]\n'
                unitsText += '\t[ClassData(typeof(ScalarDataset))]\n'
                unitsText += '\tpublic void In' + unit.name + '(Scalar expected)\n'
                unitsText += '\t{\n'
                unitsText += '\t\t#Quantity# quantity = new(expected, #Unit#.' + unit.name + ');\n\n'
                unitsText += '\t\tScalar actual = quantity.' + parseUnitPlural(unit.name, unit.plural) + ';\n\n'
                unitsText += '\t\tUtility.AssertExtra.AssertEqualMagnitudes(expected, actual);\n'
                unitsText += '\t}\n\n'
            }
        }

        return unitsText.slice(0, -2)
    }

    private composeConstantMultiplesText(scalar: ScalarQuantity): string {
        let multiplesText: string = ''
        
        const constants: Unit['constants'] = getConstants(this.definitionReader.definitions, scalar)
        for (let constant of constants) {
            if (!constant.special) {
                multiplesText += '\t[Theory]\n'
                multiplesText += '\t[ClassData(typeof(ScalarDataset))]\n'
                multiplesText += '\tpublic void In' + constant.name + '(Scalar expected)\n'
                multiplesText += '\t{\n'
                multiplesText += '\t\t#Quantity# quantity = new(expected, #Unit#.' + constant.name + ');\n\n'
                multiplesText += '\t\tScalar actual = quantity.' + constant.name + 'Multiples;\n\n'
                multiplesText += '\t\tUtility.AssertExtra.AssertEqualMagnitudes(expected, actual);\n'
                multiplesText += '\t}\n\n'
            }
        }

        return multiplesText.slice(0, -2)
    }

    private insertNames(text: string, scalar: ScalarQuantity): string {
        const unit: Unit = getUnit(this.definitionReader.definitions, scalar)
        text = text.replace(/#Unit#/g, 'UnitOf' + unit.name)

        text = text.replace(/#UnitQuantity#/g, getUnitQuantity(this.definitionReader.definitions, unit).name)
        text = text.replace(/#UnbiasedQuantity#/g, unit.unbiasedQuantity ? unit.unbiasedQuantity : 'NoUnbiasedQuantityError')

        text = text.replace(/#Quantity#/g, scalar.name)
        text = text.replace(/#InverseQuantity#/g, scalar.inverse && scalar.inverse.length > 0 ? scalar.inverse[0] : 'NoInverseQuantityError')

        const vector: VectorQuantity | undefined = getVectorVersionOfScalar(this.definitionReader.definitions, scalar)
        if (vector) {
            text = text.replace(/#VectorQuantity#/g, vector.name)
        }

        return text
    }

    private setConditionalBlocks(scalar: ScalarQuantity, text: string): string {
        if (scalar.unitBias) {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)Biased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#BiasedUnit#([^]+?)#\/BiasedUnit#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Unbiased#([^]+?)#\/Unbiased#/g, '')
        } else if (getUnit(this.definitionReader.definitions, scalar).bias) {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)BiasedUnit#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Biased#([^]+?)#\/Biased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Unbiased#([^]+?)#\/Unbiased#/g, '')
        } else {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)Unbiased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Biased#([^]+?)#\/Biased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#BiasedUnit#([^]+?)#\/BiasedUnit#/g, '')
        }

        if (scalar.inverse !== undefined && scalar.inverse.length > 0) {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)Invert#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#NoInvert#([^]+?)#\/NoInvert#/g, '')
        } else {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)NoInvert#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Invert#([^]+?)#\/Invert#/g, '')
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