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
            this.generateDataset(this.definitionReader.definitions.units[key])
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

    private insertNames(text: string, unit: Unit): string {
        text = text.replace(/#Unit#/g, 'UnitOf' + unit.name)

        const quantity: ScalarQuantity = getUnitQuantity(this.definitionReader.definitions, unit)
        text = text.replace(/#Quantity#/g, quantity.name)

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