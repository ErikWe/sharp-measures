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
        cast: "",
        comparison: "",
        constructor: "",
        convertible: "",
        dataset: "",
        equality: "",
        inUnits: "",
        mathFunctions: "",
        mathOperations: "",
        mathPowers: "",
        properties: "",
        toVectorN: ""
    }

    public vectorTests = {
        cast: "",
        constructor: "",
        cross: "",
        convertible: "",
        dataset: "",
        dot: "",
        inUnits: "",
        magnitude: "",
        mathOperations: "",
        normalize: "",
        transform: ""
    }

    public unitTests = {
        comparison: "",
        constructor: "",
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

        this.scalarTests.cast = await fsp.readFile(scalarTestsPath + 'Cast.txt', { encoding: 'utf-8' })
        this.scalarTests.comparison = await fsp.readFile(scalarTestsPath + 'Comparison.txt', { encoding: 'utf-8' })
        this.scalarTests.constructor = await fsp.readFile(scalarTestsPath + 'Constructor.txt', { encoding: 'utf-8' })
        this.scalarTests.convertible = await fsp.readFile(scalarTestsPath + 'Convertible.txt', { encoding: 'utf-8' })
        this.scalarTests.dataset = await fsp.readFile(scalarTestsPath + 'Dataset.txt', { encoding: 'utf-8' })
        this.scalarTests.equality = await fsp.readFile(scalarTestsPath + 'Equality.txt', { encoding: 'utf-8' })
        this.scalarTests.inUnits = await fsp.readFile(scalarTestsPath + 'InUnits.txt', { encoding: 'utf-8' })
        this.scalarTests.mathFunctions = await fsp.readFile(scalarTestsPath + 'MathFunctions.txt', { encoding: 'utf-8' })
        this.scalarTests.mathOperations = await fsp.readFile(scalarTestsPath + 'MathOperations.txt', { encoding: 'utf-8' })
        this.scalarTests.mathPowers = await fsp.readFile(scalarTestsPath + 'MathPowers.txt', { encoding: 'utf-8' })
        this.scalarTests.properties = await fsp.readFile(scalarTestsPath + 'Properties.txt', { encoding: 'utf-8' })
        this.scalarTests.toVectorN = await fsp.readFile(scalarTestsPath + 'ToVectorN.txt', { encoding: 'utf-8' })

        const vectorTestsPath: PathLike = process.cwd() + '\\' + options.templates + '\\VectorTests\\'

        this.vectorTests.cast = await fsp.readFile(vectorTestsPath + 'Cast.txt', { encoding: 'utf-8' })
        this.vectorTests.constructor = await fsp.readFile(vectorTestsPath + 'Constructor.txt', { encoding: 'utf-8' })
        this.vectorTests.cross = await fsp.readFile(vectorTestsPath + 'Cross.txt', { encoding: 'utf-8' })
        this.vectorTests.convertible = await fsp.readFile(vectorTestsPath + 'Convertible.txt', { encoding: 'utf-8' })
        this.vectorTests.dataset = await fsp.readFile(vectorTestsPath + 'Dataset.txt', { encoding: 'utf-8' })
        this.vectorTests.dot = await fsp.readFile(vectorTestsPath + 'Dot.txt', { encoding: 'utf-8' })
        this.vectorTests.inUnits = await fsp.readFile(vectorTestsPath + 'InUnits.txt', { encoding: 'utf-8' })
        this.vectorTests.magnitude = await fsp.readFile(vectorTestsPath + 'Magnitude.txt', { encoding: 'utf-8' })
        this.vectorTests.mathOperations = await fsp.readFile(vectorTestsPath + 'MathOperations.txt', { encoding: 'utf-8' })
        this.vectorTests.normalize = await fsp.readFile(vectorTestsPath + 'Normalize.txt', { encoding: 'utf-8' })
        this.vectorTests.transform = await fsp.readFile(vectorTestsPath + 'Transform.txt', { encoding: 'utf-8' })

        const unitTestsPath: PathLike = process.cwd() + '\\' + options.templates + '\\UnitTests\\'

        this.unitTests.comparison = await fsp.readFile(unitTestsPath + 'Comparison.txt', { encoding: 'utf-8' })
        this.unitTests.constructor = await fsp.readFile(unitTestsPath + 'Constructor.txt', { encoding: 'utf-8' })
        this.unitTests.dataset = await fsp.readFile(unitTestsPath + 'Dataset.txt', { encoding: 'utf-8' })
    }
}