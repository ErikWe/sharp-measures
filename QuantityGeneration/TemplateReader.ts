import fsp from 'fs/promises'
import { CommandLineOptions } from 'command-line-args'
import { PathLike } from 'fs'

export class TemplateReader {

    public static async Construct(options: CommandLineOptions): Promise<TemplateReader> {
        const instance: TemplateReader = new TemplateReader()
        await instance.init(options)
        return instance
    }

    public scalarTemplate: string = ""
    public vectorTemplate: string = ""
    public unitTemplate: string = ""
    public biasedUnitTemplate: string = ""

    private constructor() {} // nosonar: 'private' modifier means that no public constructor is generated.

    private async init(options: CommandLineOptions): Promise<void> {
        await this.readTemplates(options)
    }

    private async readTemplates(options: CommandLineOptions): Promise<void> {
        const scalarTemplatePath: PathLike = process.cwd() + '\\' + options.templates + '\\Scalar.txt'
        const vectorTemplatePath: PathLike = process.cwd() + '\\' + options.templates + '\\Vector.txt'
        const unitTemplatePath: PathLike = process.cwd() + '\\' + options.templates + '\\Unit.txt'
        const biasedUnitTemplatePath: PathLike = process.cwd() + '\\' + options.templates + '\\BiasedUnit.txt'

        this.scalarTemplate = await fsp.readFile(scalarTemplatePath, { encoding: 'utf-8' })
        this.vectorTemplate = await fsp.readFile(vectorTemplatePath, { encoding: 'utf-8' })
        this.unitTemplate = await fsp.readFile(unitTemplatePath, { encoding: 'utf-8' })
        this.biasedUnitTemplate = await fsp.readFile(biasedUnitTemplatePath, { encoding: 'utf-8' })
    }
}