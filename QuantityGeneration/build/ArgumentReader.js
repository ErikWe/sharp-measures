"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ArgumentReader = void 0;
const command_line_args_1 = __importDefault(require("command-line-args"));
const command_line_usage_1 = __importDefault(require("command-line-usage"));
class ArgumentReader {
    constructor(displayHelpIfRelevant) {
        this.definitions = [
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
        ];
        this.usage = (0, command_line_usage_1.default)([
            {
                header: 'Quantity Generator',
                content: 'Generates quantities and documentation.'
            },
            {
                header: 'Options',
                optionList: this.definitions
            }
        ]);
        this.options = (0, command_line_args_1.default)(this.definitions);
        if (displayHelpIfRelevant && this.options.help) {
            this.displayHelp();
        }
    }
    displayHelp() {
        console.log(this.usage);
    }
}
exports.ArgumentReader = ArgumentReader;
//# sourceMappingURL=ArgumentReader.js.map