import fsp from 'fs/promises'
import { ArgumentReader } from './ArgumentReader'
import { DefinitionReader } from './DefinitionReader'
import { TemplateReader } from './TemplateReader'
import { ScalarGenerator } from './ScalarGenerator'
import { ScalarTestsGenerator } from './ScalarTestsGenerator'
import { VectorGenerator } from './VectorGenerator'
import { VectorTestsGenerator } from './VectorTestsGenerator'
import { UnitGenerator } from './UnitGenerator'
import { UnitTestsGenerator } from './UnitTestsGenerator'

class Program {

    static async main() {
        const argumentReader: ArgumentReader = new ArgumentReader(true)
        const definitionReader: DefinitionReader = await DefinitionReader.Construct(argumentReader.options)
        const templateReader: TemplateReader = await TemplateReader.Construct(argumentReader.options)
   
        const documentationDirectory: string = process.cwd() + '\\' + argumentReader.options.documentation
        
        const quantityDestination: string = process.cwd() + '\\' + argumentReader.options.quantityDestination
        const quantityDestinationOK: boolean = await this.createDestination(quantityDestination, argumentReader.options.DESTROY === true)
        if (quantityDestinationOK) {
            await this.createScalars(quantityDestination, documentationDirectory, definitionReader, templateReader)
            await this.createVectors(quantityDestination, documentationDirectory, definitionReader, templateReader)
        }
        
        const unitDestination: string = process.cwd() + '\\' + argumentReader.options.unitDestination
        const unitDestinationOK: boolean = await this.createDestination(unitDestination, argumentReader.options.DESTROY === true)
        if (unitDestinationOK) {
            await this.createUnits(unitDestination, documentationDirectory, definitionReader, templateReader)
        }

        const testDestination: string = process.cwd() + '\\' + argumentReader.options.testDestination
        const testDestinationOK: boolean = await this.createDestination(testDestination, argumentReader.options.DESTROY === true)
        if (testDestinationOK) {
            await this.createTests(testDestination, definitionReader, templateReader, argumentReader.options.DESTROY === true)
        }
    }

    private static async createScalars(destination: string, documentationDirectory: string, definitionReader: DefinitionReader, templateReader: TemplateReader) {
        new ScalarGenerator(destination, documentationDirectory, definitionReader, templateReader).generate()
    }

    private static async createVectors(destination: string, documentationDirectory: string, definitionReader: DefinitionReader, templateReader: TemplateReader) {
        new VectorGenerator(destination, documentationDirectory, definitionReader, templateReader).generate()
    }

    private static async createUnits(destination: string, documentationDirectory: string, definitionReader: DefinitionReader, templateReader: TemplateReader) {
        new UnitGenerator(destination, documentationDirectory, definitionReader, templateReader).generate()
    }

    private static async createTests(destination: string, definitionReader: DefinitionReader, templateReader: TemplateReader, destroy: boolean) {
        new ScalarTestsGenerator(destination, definitionReader, templateReader, destroy).generate()
        new VectorTestsGenerator(destination, definitionReader, templateReader, destroy).generate()
        new UnitTestsGenerator(destination, definitionReader, templateReader, destroy).generate()
    }

    private static async createDestination(destination: string, destroy: boolean) : Promise<boolean> {
        if (await this.pathExists(destination)) {
            if (!destroy) {
                const items = await fsp.readdir(destination)
                if (items.length > 0) {
                    console.log('The directory [' + destination + '] is not empty. To continue and potentially LOSE EXISTING CONTENT, add the flag \'-DESTROY\'')
                    return false
                }
            }
        }

        await fsp.mkdir(destination, { recursive: true })
        return true
    }

    private static async pathExists(path: string) : Promise<boolean> {
        return fsp.stat(path).then(() => true, () => false)
    }
}

Program.main()