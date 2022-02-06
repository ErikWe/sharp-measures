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
exports.readTag = void 0;
const promises_1 = __importDefault(require("fs/promises"));
const path_1 = __importDefault(require("path"));
const readTag = (tag, fileName, searchedFiles = undefined) => __awaiter(void 0, void 0, void 0, function* () {
    if (searchedFiles === undefined) {
        searchedFiles = [];
    }
    const text = yield readFile(fileName, 'utf-8');
    if (text === undefined) {
        reportErrorFileNotFound(fileName);
        return false;
    }
    const matched = extractTag(tag, text);
    searchedFiles.push(fileName);
    if (matched === false) {
        return readTagFromExtension(tag, fileName, searchedFiles);
    }
    else {
        return matched;
    }
});
exports.readTag = readTag;
const readTagFromExtension = (tag, fileName, searchedFiles) => __awaiter(void 0, void 0, void 0, function* () {
    const regex = /#Extends:([A-Za-z\d_\-.]+?)(?:\r\n|\n|#)\s/g;
    const text = yield promises_1.default.readFile(fileName, { encoding: 'utf-8' });
    const extending = text.matchAll(regex);
    if (extending === null) {
        return false;
    }
    for (let extension of extending) {
        const nextFileName = path_1.default.dirname(fileName) + '\\' + extension[1];
        if (!searchedFiles.includes(nextFileName)) {
            const matched = yield (0, exports.readTag)(tag, nextFileName, searchedFiles);
            if (matched !== false) {
                return matched;
            }
        }
    }
    return false;
});
const extractTag = (tag, text) => {
    const regex = new RegExp('#Define:' + tag + '(\\r\\n|\\n|#|\\([A-z0-9, _\\-]*\\))(?:#?)(?:\\n|\\r\\n|\\s*?)([^]+?)(?:\\r\\n|\\n|\\s*)#\\/', 's');
    const regexResult = text.match(regex);
    if (regexResult === null) {
        return false;
    }
    const content = regexResult[2];
    const parameters = regexResult[1].includes('(') ? regexResult[1].slice(1, -1).split(',') : [];
    for (let i = 0; i < parameters.length; i++) {
        parameters[i] = parameters[i].trim();
    }
    return { content: content, parameters: parameters };
};
const readFile = (fileName, encoding) => __awaiter(void 0, void 0, void 0, function* () {
    if (yield promises_1.default.stat(fileName).catch(() => false)) {
        return promises_1.default.readFile(fileName, { encoding: 'utf-8' });
    }
    else {
        return undefined;
    }
});
const reportErrorFileNotFound = (fileName) => {
    console.error('File was not found: [' + fileName + '].');
};
//# sourceMappingURL=DocumentationReader.js.map