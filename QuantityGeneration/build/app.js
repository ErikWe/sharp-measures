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
const promises_1 = __importDefault(require("fs/promises"));
const ArgumentReader_1 = require("./ArgumentReader");
const DefinitionReader_1 = require("./DefinitionReader");
const TemplateReader_1 = require("./TemplateReader");
const ScalarGenerator_1 = require("./ScalarGenerator");
const VectorGenerator_1 = require("./VectorGenerator");
class Program {
    static main() {
        return __awaiter(this, void 0, void 0, function* () {
            const argumentReader = new ArgumentReader_1.ArgumentReader(true);
            const definitionReader = yield DefinitionReader_1.DefinitionReader.Construct(argumentReader.options);
            const templateReader = yield TemplateReader_1.TemplateReader.Construct(argumentReader.options);
            const destination = process.cwd() + '\\' + argumentReader.options.destination;
            const destinationOK = yield this.createDestination(destination, argumentReader.options.DESTROY === true);
            const documentationDirectory = process.cwd() + '\\' + argumentReader.options.documentation;
            if (destinationOK) {
                yield this.createScalars(destination, documentationDirectory, definitionReader, templateReader);
                yield this.createVectors(destination, documentationDirectory, definitionReader, templateReader);
            }
        });
    }
    static createScalars(destination, documentationDirectory, definitionReader, templateReader) {
        return __awaiter(this, void 0, void 0, function* () {
            new ScalarGenerator_1.ScalarGenerator(destination, documentationDirectory, definitionReader, templateReader).generate();
        });
    }
    static createVectors(destination, documentationDirectory, definitionReader, templateReader) {
        return __awaiter(this, void 0, void 0, function* () {
            new VectorGenerator_1.VectorGenerator(destination, documentationDirectory, definitionReader, templateReader).generate();
        });
    }
    static createDestination(destination, destroy) {
        return __awaiter(this, void 0, void 0, function* () {
            if (yield this.pathExists(destination)) {
                if (!destroy) {
                    const items = yield promises_1.default.readdir(destination);
                    if (items.length > 0) {
                        console.log('The directory [' + destination + '] is not empty. To continue and LOSE EXISTING CONTENT, add the flag \'-DESTROY\'');
                        return false;
                    }
                }
                else {
                    yield promises_1.default.rm(destination, { recursive: true, force: true });
                }
            }
            yield promises_1.default.mkdir(destination, { recursive: true });
            return true;
        });
    }
    static pathExists(path) {
        return __awaiter(this, void 0, void 0, function* () {
            return promises_1.default.stat(path).then(() => true, () => false);
        });
    }
}
Program.main();
//# sourceMappingURL=app.js.map