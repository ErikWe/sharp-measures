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
exports.TemplateReader = void 0;
const promises_1 = __importDefault(require("fs/promises"));
class TemplateReader {
    constructor() {
        this.scalarTemplate = "";
        this.vectorTemplate = "";
    }
    static Construct(options) {
        return __awaiter(this, void 0, void 0, function* () {
            const instance = new TemplateReader();
            yield instance.init(options);
            return instance;
        });
    }
    init(options) {
        return __awaiter(this, void 0, void 0, function* () {
            yield this.readTemplates(options);
        });
    }
    readTemplates(options) {
        return __awaiter(this, void 0, void 0, function* () {
            const scalarTemplatePath = process.cwd() + '\\' + options.scalarTemplate;
            const vectorTemplatePath = process.cwd() + '\\' + options.vectorTemplate;
            this.scalarTemplate = yield promises_1.default.readFile(scalarTemplatePath, { encoding: 'utf-8' });
            this.vectorTemplate = yield promises_1.default.readFile(vectorTemplatePath, { encoding: 'utf-8' });
        });
    }
}
exports.TemplateReader = TemplateReader;
//# sourceMappingURL=TemplateReader.js.map