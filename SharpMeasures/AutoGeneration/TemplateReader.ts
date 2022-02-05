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

    private constructor() {}

    private async init(options: CommandLineOptions): Promise<void> {
        await this.readTemplates(options)
    }

    private async readTemplates(options: CommandLineOptions): Promise<void> {
        const scalarTemplatePath: PathLike = process.cwd() + '\\' + options.scalarTemplate
        const vectorTemplatePath: PathLike = process.cwd() + '\\' + options.vectorTemplate

        this.scalarTemplate = await fsp.readFile(scalarTemplatePath, { encoding: 'utf-8' })
        this.vectorTemplate = await fsp.readFile(vectorTemplatePath, { encoding: 'utf-8' })
    }
}