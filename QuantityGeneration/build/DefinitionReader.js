"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.DefinitionReader = void 0;
const promises_1 = __importDefault(require("fs/promises"));
class DefinitionReader {
    constructor() {
        this.definitions = {
            scalars: {},
            vectors: {}
        };
    } // nosonar
    static Construct(options) {
        return __awaiter(this, void 0, void 0, function* () {
            const instance = new DefinitionReader();
            yield instance.init(options);
            return instance;
        });
    }
    init(options) {
        return __awaiter(this, void 0, void 0, function* () {
            const definitionsDirectory = process.cwd() + '\\' + options.definitions;
            yield this.readDefinitions(definitionsDirectory);
        });
    }
    readDefinitions(directory) {
        return __awaiter(this, void 0, void 0, function* () {
            const items = yield promises_1.default.readdir(directory);
            yield Promise.all(items.map((item) => __awaiter(this, void 0, void 0, function* () {
                if ((yield promises_1.default.stat(directory + '\\' + item)).isDirectory()) {
                    this.readDefinitions(directory + '\\' + item);
                }
                else {
                    let content = yield promises_1.default.readFile(directory + '\\' + item, { encoding: 'utf-8' });
                    if (content.charAt(0) === '\uFEFF') {
                        content = content.substring(1);
                    }
                    const jsonData = JSON.parse(content);
                    this.appendDefinition(jsonData, directory + '\\' + item);
                }
            })));
        });
    }
    appendDefinition(jsonData, originFile) {
        return __awaiter(this, void 0, void 0, function* () {
            if (!('type' in jsonData && 'name' in jsonData)) {
                if ('name' in jsonData) {
                    this.reportErrorMissingType(jsonData, originFile);
                }
                else {
                    this.reportErrorMissingName(originFile);
                }
                return;
            }
            if (jsonData.name != originFile.split('\\').slice(-1)[0].split('.')[0]) {
                this.reportWarningNameMismatch(jsonData, originFile);
            }
            if (jsonData.type == 'Scalar') {
                this.definitions.scalars[jsonData.name] = jsonData;
            }
            else if (jsonData.type == 'Vector') {
                this.definitions.vectors[jsonData.name] = jsonData;
            }
            else {
                this.reportErrorTypeParsing(jsonData, originFile);
            }
        });
    }
    reportErrorMissingName(originFile) {
        console.error('Quantity definition is missing entry [name], in file: [' + originFile + '].');
    }
    reportErrorMissingType(jsonData, originFile) {
        console.error('Quantity definition is missing entry [type]: [' + jsonData.name + '] in file: [' + originFile + '].');
    }
    reportErrorTypeParsing(jsonData, originFile) {
        console.error('Could not parse quantity type: [' + jsonData.name + '] of type: [' + jsonData.type + '] in file: [' + originFile + '].');
    }
    reportWarningNameMismatch(jsonData, originFile) {
        console.warn('Quantity name does not match file name: [' + jsonData.name + '] in file: [' + originFile + '].');
    }
}
exports.DefinitionReader = DefinitionReader;
//# sourceMappingURL=DefinitionReader.js.map