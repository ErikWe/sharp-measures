import fsp from 'fs/promises'
import path from 'path'
import { DefinitionReader } from './DefinitionReader'
import { TemplateReader } from './TemplateReader'
import { ScalarQuantity } from './ScalarQuantity'
import { Unit } from './Unit'
import { fixLines, getBases, getConstants, getUnit, getUnitQuantity } from './Utility'

export class ScalarTestsGenerator {

    public constructor(private destination: string, private definitionReader: DefinitionReader, private templateReader: TemplateReader, private destroy: boolean) {}

    public async generate(): Promise<void> {
        const keys: string[] = Object.keys(this.definitionReader.definitions.scalars)

        await Promise.all(keys.map(async (key: string) => {
            this.generateDataset(this.definitionReader.definitions.scalars[key])
            this.generateCastTests(this.definitionReader.definitions.scalars[key])
            this.generateConstructorTests(this.definitionReader.definitions.scalars[key])
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

    private async generateConstructorTests(scalar: ScalarQuantity): Promise<void> {
        let text: string = this.templateReader.scalarTests.constructor

        text = this.setConditionalBlocks(scalar, text)

        const constantsText: string = this.composeConstructorConstantsText(scalar)
        text = text.replace(/#Constants#/g, constantsText)

        const basesText: string = this.composeConstructorBasesText(scalar)
        text = text.replace(/#Bases#/g, basesText)

        text = this.insertNames(text, scalar)

        text = fixLines(text)

        this.attemptWriteFile(this.destination + '\\Cases\\QuantityTests\\' + scalar.name + 'Tests\\Generated\\ConstructorTests.g.cs', text)
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

    private insertNames(text: string, scalar: ScalarQuantity): string {
        const unit: Unit = getUnit(this.definitionReader.definitions, scalar)
        text = text.replace(/#Unit#/g, 'UnitOf' + unit.name)

        text = text.replace(/#UnitQuantity#/g, getUnitQuantity(this.definitionReader.definitions, unit).name)
        text = text.replace(/#UnbiasedQuantity#/g, unit.unbiasedQuantity ? unit.unbiasedQuantity : 'NoUnbiasedQuantityError')

        text = text.replace(/#Quantity#/g, scalar.name)

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