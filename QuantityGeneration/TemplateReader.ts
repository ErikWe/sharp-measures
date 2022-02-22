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

    public scalarTests = {
        dataset: "",
        cast: "",
        comparison: "",
        constructor: "",
        equality: "",
        mathFunctions: "",
        mathOperations: "",
        mathPowers: "",
        properties: ""
    }

    public unitTests = {
        dataset: ""
    }

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

        const scalarTestsPath: PathLike = process.cwd() + '\\' + options.templates + '\\ScalarTests\\'

        this.scalarTests.dataset = await fsp.readFile(scalarTestsPath + 'Dataset.txt', { encoding: 'utf-8' })
        this.scalarTests.cast = await fsp.readFile(scalarTestsPath + 'Cast.txt', { encoding: 'utf-8' })
        this.scalarTests.comparison = await fsp.readFile(scalarTestsPath + 'Comparison.txt', { encoding: 'utf-8' })
        this.scalarTests.constructor = await fsp.readFile(scalarTestsPath + 'Constructor.txt', { encoding: 'utf-8' })
        this.scalarTests.equality = await fsp.readFile(scalarTestsPath + 'Equality.txt', { encoding: 'utf-8' })
        this.scalarTests.mathFunctions = await fsp.readFile(scalarTestsPath + 'MathFunctions.txt', { encoding: 'utf-8' })
        this.scalarTests.mathOperations = await fsp.readFile(scalarTestsPath + 'MathOperations.txt', { encoding: 'utf-8' })
        this.scalarTests.mathPowers = await fsp.readFile(scalarTestsPath + 'MathPowers.txt', { encoding: 'utf-8' })
        this.scalarTests.properties = await fsp.readFile(scalarTestsPath + 'Properties.txt', { encoding: 'utf-8' })

        const unitTestsPath: PathLike = process.cwd() + '\\' + options.templates + '\\UnitTests\\'

        this.unitTests.dataset = await fsp.readFile(unitTestsPath + 'Dataset.txt', { encoding: 'utf-8' })
    }
}