import fsp from 'fs/promises'
import { ArgumentReader } from './ArgumentReader'
import { DefinitionReader } from './DefinitionReader'
import { TemplateReader } from './TemplateReader'
import { ScalarGenerator } from './ScalarGenerator'
import { VectorGenerator } from './VectorGenerator'

class Program {

    static async main() {
        const argumentReader: ArgumentReader = new ArgumentReader(true)
        const definitionReader: DefinitionReader = await DefinitionReader.Construct(argumentReader.options)
        const templateReader: TemplateReader = await TemplateReader.Construct(argumentReader.options)

        const destination: string = process.cwd() + '\\' + argumentReader.options.destination
        const destinationOK: boolean = await this.createDestination(destination, argumentReader.options.DESTROY === true)

        const documentationDirectory: string = process.cwd() + '\\' + argumentReader.options.documentation

        if (destinationOK) {
            await this.createScalars(destination, documentationDirectory, definitionReader, templateReader)
            await this.createVectors(destination, documentationDirectory, definitionReader, templateReader)
        }
    }

    private static async createScalars(destination: string, documentationDirectory: string, definitionReader: DefinitionReader, templateReader: TemplateReader) {
        new ScalarGenerator(destination, documentationDirectory, definitionReader, templateReader).generate()
    }

    private static async createVectors(destination: string, documentationDirectory: string, definitionReader: DefinitionReader, templateReader: TemplateReader) {
        new VectorGenerator(destination, documentationDirectory, definitionReader, templateReader).generate()
    }

    private static async createDestination(destination: string, destroy: boolean) : Promise<boolean> {
        if (await this.pathExists(destination)) {
            if (!destroy) {
                const items = await fsp.readdir(destination)
                if (items.length > 0) {
                    console.log('The directory [' + destination + '] is not empty. To continue and LOSE EXISTING CONTENT, add the flag \'-DESTROY\'')
                    return false
                }
            } else {
                await fsp.rm(destination, { recursive: true, force: true })
            }
        }

        await fsp.mkdir(destination)
        return true
    }

    private static async pathExists(path: string) : Promise<boolean> {
        return await fsp.stat(path).then(() => true, () => false)
    }
}

Program.main()