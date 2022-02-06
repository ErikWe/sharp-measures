import commandLineArgs, { CommandLineOptions } from 'command-line-args'
import commandLineUsage from 'command-line-usage'

export class ArgumentReader {

    public options: CommandLineOptions

    public constructor(displayHelpIfRelevant: boolean) {
        this.options = commandLineArgs(this.definitions)
    
        if (displayHelpIfRelevant && this.options.help) {
            this.displayHelp()
        }
    }

    private displayHelp(): void {
        console.log(this.usage)
    }

    private definitions = [
        {
            name: 'help',
            alias: 'h',
            type: Boolean,
            description: 'Display this usage guide.'
        },
        {
            name: 'destination',
            type: String,
            defaultValue: 'Quantities\\Generated',
            description: 'Relative path of destination directory.'
        },
        {
            name: 'definitions',
            type: String,
            defaultValue: 'AutoGeneration\\Definitions',
            description: 'Relative path of the directory containing quantity definitions.'
        },
        {
            name: 'documentation',
            type: String,
            defaultValue: 'AutoGeneration\\Documentation',
            description: 'Relative path of the directory containing quantity documentation'
        },
        {
            name: 'scalarTemplate',
            type: String,
            defaultValue: 'AutoGeneration\\Templates\\ScalarTemplate.txt',
            description: 'Relative path of scalar template file.'
        },
        {
            name: 'vectorTemplate',
            type: String,
            defaultValue: 'AutoGeneration\\Templates\\VectorTemplate.txt',
            description: 'Relative path of vector template file'
        },
        {
            name: 'DESTROY',
            type: Boolean,
            defaultValue: false,
            description: 'Enable overwriting existing content.'
        }
    ]

    private usage: string = commandLineUsage([
        {
            header: 'Quantity Generator',
            content: 'Generates quantities and documentation.'
        },
        {
            header: 'Options',
            optionList: this.definitions
        }
    ])
}