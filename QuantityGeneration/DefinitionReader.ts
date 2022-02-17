import fsp from 'fs/promises'
import { CommandLineOptions } from 'command-line-args'
import { ScalarQuantity } from './ScalarQuantity'
import { VectorQuantity } from './VectorQuantity'
import { Unit } from './Unit'
import { QuantityDefinitions } from './Utility'

export class DefinitionReader {

    public static async Construct(options: CommandLineOptions): Promise<DefinitionReader> {
        const instance: DefinitionReader = new DefinitionReader()
        await instance.init(options)
        return instance
    }

    public definitions: QuantityDefinitions = {
        scalars: {},
        vectors: {},
        units: {}
    }

    private constructor() { } // nosonar: 'private' modifier means that no public constructor is generated.

    private async init(options: CommandLineOptions): Promise<void> {
        const definitionsDirectory: string = process.cwd() + '\\' + options.definitions
        await this.readDefinitions(definitionsDirectory)
    }

    private async readDefinitions(directory: string): Promise<void> {
        const items: string[] = await fsp.readdir(directory)

        await Promise.all(items.map(async (item: string) => {
            if ((await fsp.stat(directory + '\\' + item)).isDirectory()) {
                this.readDefinitions(directory + '\\' + item)
            } else {
                let content: string = await fsp.readFile(directory + '\\' + item, { encoding: 'utf-8' })
                if (content.charAt(0) === '\uFEFF') {
                    content = content.substring(1);
                }

                const jsonData: GeneratedType = JSON.parse(content)
                this.appendDefinition(jsonData, directory + '\\' + item)
            }
        }));
    }

    private async appendDefinition(jsonData: GeneratedType, originFile: string): Promise<void> {
        if (!('type' in jsonData && 'name' in jsonData)) {
            if ('name' in jsonData) {
                this.reportErrorMissingType(jsonData, originFile)
            } else {
                this.reportErrorMissingName(originFile)
            }
            return
        }

        if (jsonData.name != originFile.split('\\').slice(-1)[0].split('.')[0]) {
            this.reportWarningNameMismatch(jsonData, originFile)
        }

        if (jsonData.type == 'Scalar') {
            this.definitions.scalars[jsonData.name] = jsonData
        } else if (jsonData.type == 'Vector') {
            this.definitions.vectors[jsonData.name] = jsonData
        } else if (jsonData.type == 'Unit') {
            this.definitions.units[jsonData.name] = jsonData
        } else {
            this.reportErrorTypeParsing(jsonData, originFile)
        }
    }

    private reportErrorMissingName(originFile: string): void {
        console.error('Quantity definition is missing entry [name], in file: [' + originFile + '].')
    }

    private reportErrorMissingType(jsonData: GeneratedType, originFile: string): void {
        console.error('Quantity definition is missing entry [type]: [' + jsonData.name + '] in file: [' + originFile + '].')
    }

    private reportErrorTypeParsing(jsonData: GeneratedType, originFile: string): void {
        console.error('Could not parse quantity type: [' + jsonData.name + '] of type: [' + jsonData.type + '] in file: [' + originFile + '].')
    }

    private reportWarningNameMismatch(jsonData: GeneratedType, originFile: string): void {
        console.warn('Quantity name does not match file name: [' + jsonData.name + '] in file: [' + originFile + '].')
    }
}

type GeneratedType = ScalarQuantity | VectorQuantity | Unit