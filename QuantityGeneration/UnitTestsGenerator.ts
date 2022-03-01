import fsp from 'fs/promises'
import path from 'path'
import { DefinitionReader } from './DefinitionReader'
import { Unit } from './Unit'
import { TemplateReader } from './TemplateReader'
import { getUnitQuantity, fixLines } from './Utility'
import { ScalarQuantity } from './ScalarQuantity'

export class UnitTestsGenerator {
    public constructor(private destination: string, private definitionReader: DefinitionReader, private templateReader: TemplateReader, private destroy: boolean) {}

    public async generate(): Promise<void> {
        const keys: string[] = Object.keys(this.definitionReader.definitions.units)

        await Promise.all(keys.map(async (key: string) => {
            const unit: Unit = this.definitionReader.definitions.units[key]
            this.generateDataset(unit)
            this.generateConstructorTests(unit)

            if (!unit.bias) {
                this.generateComparisonTests(unit)
            }
        }))
    }

    private async generateDataset(unit: Unit): Promise<void> {
        let text: string = this.templateReader.unitTests.dataset

        const unitsText: string = this.composeDatasetUnitsText(unit)
        text = text.replace(/#Units#/g, unitsText)

        text = this.insertNames(text, unit)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Datasets\\Generated\\UnitOf' + unit.name + 'Dataset.g.cs', text)
    }

    private async generateComparisonTests(unit: Unit): Promise<void> {
        let text: string = this.templateReader.unitTests.comparison

        text = this.insertNames(text, unit)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\UnitTests\\' + unit.name + 'Tests\\Generated\\ComparisonTests.g.cs', text)
    }

    private async generateConstructorTests(unit: Unit): Promise<void> {
        let text: string = this.templateReader.unitTests.constructor

        text = this.setConditionalBlocks(unit, text)
        text = this.insertNames(text, unit)
        text = fixLines(text)
        this.attemptWriteFile(this.destination + '\\Cases\\UnitTests\\' + unit.name + 'Tests\\Generated\\ConstructorTests.g.cs', text)
    }

    private insertNames(text: string, unit: Unit): string {
        text = text.replace(/#Unit#/g, 'UnitOf' + unit.name)

        const quantity: ScalarQuantity = getUnitQuantity(this.definitionReader.definitions, unit)
        text = text.replace(/#Quantity#/g, quantity.name)

        text = text.replace(/#UnbiasedQuantity#/g, unit.unbiasedQuantity ? unit.unbiasedQuantity : 'NoUnbiasedQuantityError')

        return text
    }

    private composeDatasetUnitsText(unit: Unit): string {
        let unitsText: string = ''

        for (let unitDefinition of unit.units) {
            if (!unitDefinition.special) {
                unitsText += '\t\t\tyield return new object?[] { #Unit#.' + unitDefinition.name + ' };\n'
            }
        }

        return unitsText.slice(0, -1)
    }

    private setConditionalBlocks(unit: Unit, text: string): string {
        if (unit.bias) {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)Biased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Unbiased#([^]+?)#\/Unbiased#/g, '')
        } else {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)Unbiased#/g, '')
            text = text.replace(/(\n|\r\n|\r?)#Biased#([^]+?)#\/Biased#/g, '')
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