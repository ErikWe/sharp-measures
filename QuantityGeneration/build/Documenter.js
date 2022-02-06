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
Object.defineProperty(exports, "__esModule", { value: true });
exports.Documenter = void 0;
const DocumentationReader_1 = require("./DocumentationReader");
class Documenter {
    constructor(documentationPath, passLimit = 50) {
        this.documentationPath = documentationPath;
        this.passLimit = passLimit;
    }
    static document(text, documentationPath) {
        return __awaiter(this, void 0, void 0, function* () {
            return new Documenter(documentationPath, 50).run(text);
        });
    }
    run(text) {
        return __awaiter(this, void 0, void 0, function* () {
            let i = 0;
            while (true) {
                const update = yield this.documentationPass(text);
                if (update.modified) {
                    text = update.text;
                }
                else {
                    return text;
                }
                if (++i >= this.passLimit) {
                    this.reportErrorPassLimit();
                    return text;
                }
            }
        });
    }
    documentationPass(text) {
        var _a, _b;
        return __awaiter(this, void 0, void 0, function* () {
            let rebuilt = '';
            let modified = false;
            for (let line of text.split('\n')) {
                if (line.includes('#Document:')) {
                    const indent = (_b = (_a = line.match(new RegExp('[ ]*'))) === null || _a === void 0 ? void 0 : _a[0]) !== null && _b !== void 0 ? _b : '';
                    modified = true;
                    for (let rebuiltLine of yield this.produceDocumentationLines(line)) {
                        rebuilt += indent + rebuiltLine.trim() + '\n';
                    }
                }
                else {
                    rebuilt += line + '\n';
                }
            }
            return { text: rebuilt.slice(0, -1), modified: modified };
        });
    }
    produceDocumentationLines(line) {
        return __awaiter(this, void 0, void 0, function* () {
            const invokation = this.extractDocumentationCall(line);
            if (invokation === undefined) {
                return [line];
            }
            const parsedArguments = this.extractArguments(invokation.argumentText);
            const parameterValues = {};
            let anyUnnamed = false;
            for (let parsedArgument of parsedArguments) {
                if (parsedArgument.includes('=')) {
                    const parameter = parsedArgument.split('=')[0].trim();
                    const value = parsedArgument.split('=')[1].trim();
                    parameterValues[parameter] = value;
                }
                else {
                    anyUnnamed = true;
                }
            }
            const tagData = yield (0, DocumentationReader_1.readTag)(invokation.tag, this.documentationPath);
            if (!tagData) {
                this.reportErrorUnresolvedTag(invokation.tag);
                return this.replaceDocumentationCall(invokation, line, 'UnresolvedTagError').split('\n');
            }
            if (parsedArguments.length < tagData.parameters.length) {
                this.reportErrorArgumentCount(invokation.tag, parsedArguments, tagData.parameters);
                return this.replaceDocumentationCall(invokation, line, 'ArgumentCountError').split('\n');
            }
            if (anyUnnamed) {
                if (!this.rescueUnnamedArguments(invokation.tag, parsedArguments, tagData.parameters, parameterValues)) {
                    return this.replaceDocumentationCall(invokation, line, 'ArgumentMatchingError').split('\n');
                }
            }
            yield Promise.all(Object.keys(parameterValues).map((parameter) => __awaiter(this, void 0, void 0, function* () {
                parameterValues[parameter] = yield Documenter.document(parameterValues[parameter], this.documentationPath);
            })));
            tagData.content = this.injectParameters(invokation.tag, tagData.content, tagData.parameters, parameterValues);
            for (let parameter of Object.keys(parameterValues)) {
                if (!tagData.parameters.includes(parameter)) {
                    this.reportWarningSpecifiedParameterNotPartOfSignature(invokation.tag, parameter, parameterValues[parameter]);
                }
            }
            if (tagData.content.includes('#Param:')) {
                let requestedParameters = tagData.content.matchAll(new RegExp('#Param:([A-z0-9_\-]*)(\\[(%?)([0-9]+?)\\]?)#', 'g'));
                for (let requestedParameter of requestedParameters) {
                    this.reportWarningRequestedParameterNotPartOfSignature(invokation.tag, requestedParameter[1]);
                }
            }
            return this.replaceDocumentationCall(invokation, line, tagData.content).split('\n');
        });
    }
    extractArguments(argumentText) {
        if (argumentText.trim() === '') {
            return [];
        }
        const splitIndices = [];
        let parenthesisLevel = 0;
        let bracketLevel = 0;
        for (let i = 0; i < argumentText.length; i++) {
            const character = argumentText[i];
            if (character === '(') {
                parenthesisLevel++;
            }
            else if (character === ')') {
                parenthesisLevel--;
            }
            else if (character === '[') {
                bracketLevel++;
            }
            else if (character === ']') {
                bracketLevel--;
            }
            else if (parenthesisLevel === 0 && bracketLevel === 0 && character === ',') {
                splitIndices.push(i);
            }
        }
        const parsedArguments = [];
        for (let i = 0; i < splitIndices.length; i++) {
            if (i === 0) {
                parsedArguments.push(argumentText.slice(0, splitIndices[i]).trim());
            }
            else {
                parsedArguments.push(argumentText.slice(splitIndices[i - 1] + 1, splitIndices[i]).trim());
            }
        }
        if (splitIndices.length === 0) {
            parsedArguments.push(argumentText);
        }
        else {
            parsedArguments.push(argumentText.slice(splitIndices.pop() + 1).trim());
        }
        return parsedArguments;
    }
    rescueUnnamedArguments(tag, parsedArguments, parameters, parameterValues) {
        if (Object.keys(parameterValues).length > 0) {
            return this.rescueMixedUnnamedArguments(tag, parsedArguments, parameters, parameterValues);
        }
        else {
            return this.rescueUnmixedUnnamedArguments(tag, parsedArguments, parameters, parameterValues);
        }
    }
    rescueMixedUnnamedArguments(tag, parsedArguments, parameters, parameterValues) {
        if (parsedArguments.length != parameters.length) {
            this.reportErrorArgumentCountWithUnnamedArguments(tag, parsedArguments, parameters);
            return false;
        }
        else {
            if (Object.keys(parameterValues).length == parameters.length - 1) {
                return this.rescueSingleMixedUnnamedArgument(tag, parsedArguments, parameters, parameterValues);
            }
            else {
                for (let i = 0; i < parsedArguments.length; i++) {
                    if (!parsedArguments[i].includes('=')) {
                        if (Object.keys(parameterValues).includes(parameters[i])) {
                            this.reportErrorUnmatchedUnnamedArgument(tag, parsedArguments[i]);
                            return false;
                        }
                        else {
                            parameterValues[parameters[i]] = parsedArguments[i];
                            this.reportWarningMatchedUnnamedArgument(tag, parsedArguments[i], parameters[i]);
                        }
                    }
                }
                return true;
            }
        }
    }
    rescueUnmixedUnnamedArguments(tag, parsedArguments, parameters, parameterValues) {
        if (parsedArguments.length != parameters.length) {
            this.reportErrorArgumentCountWithUnnamedArguments(tag, parsedArguments, parameters);
            return false;
        }
        else {
            for (let i = 0; i < parsedArguments.length; i++) {
                parameterValues[parameters[i]] = parsedArguments[i];
            }
            return true;
        }
    }
    rescueSingleMixedUnnamedArgument(tag, parsedArguments, parameters, parameterValues) {
        const getUnnamedArgument = (items) => {
            for (let item of items) {
                if (!item.includes('=')) {
                    return item;
                }
            }
            return undefined;
        };
        const unnamedArgument = getUnnamedArgument(parsedArguments);
        if (unnamedArgument === undefined) {
            return false;
        }
        for (let parameter of parameters) {
            if (!Object.keys(parameterValues).includes(parameter)) {
                parameterValues[parameter] = unnamedArgument;
                this.reportWarningSingleUnnamedArgument(tag, unnamedArgument, parameter);
                return true;
            }
        }
        return false;
    }
    injectParameters(tag, text, parameters, parameterValues) {
        for (let parameter of parameters) {
            if (!Object.keys(parameterValues).includes(parameter)) {
                this.reportErrorParameterNotSpecified(tag, parameter);
                text = text.replace(new RegExp('#Param:' + parameter + '#', 'g'), 'ParameterNotSpecifiedError');
            }
            else {
                text = text.replace(new RegExp('#Param:' + parameter + '#', 'g'), parameterValues[parameter]);
                let arrayAccess = text.matchAll(new RegExp('#Param:' + parameter + '\\[(%?)([0-9]+?)\\]#', 'g'));
                text = this.injectArrayParameter(tag, text, parameter, parameterValues[parameter], arrayAccess);
            }
        }
        return text;
    }
    injectArrayParameter(tag, text, parameter, parameterValue, arrayAccess) {
        if ((parameterValue.length == 0 || parameterValue[0] != '[') && arrayAccess.next().done === false) {
            this.reportErrorParameterNotArray(tag, parameter, parameterValue);
            return text.replace(new RegExp('#Param:' + parameter + '\\[(%?)([0-9]+?)\\]#', 'g'), 'ParameterNotArrayError');
        }
        else {
            const elements = parameterValue.slice(1, -1).split(',').map((element) => element.trim());
            for (let access of arrayAccess) {
                let index = Number(access[2]);
                if (access[1] == '%') {
                    text = text.replace(new RegExp('#Param:' + parameter + '\\[%' + access[2] + '\\]#', 'g'), elements[index % elements.length]);
                }
                else {
                    if (index < elements.length) {
                        text = text.replace(new RegExp('#Param:' + parameter + '\\[' + access[2] + '\\]#', 'g'), elements[index]);
                    }
                    else {
                        this.reportErrorArrayIndexOutOfBounds(tag, parameter, index, elements.length);
                        text = text.replace(new RegExp('#Param:' + parameter + '\\[([0-9]+?)\\]#', 'g'), 'ParameterIndex$1OutOfBounds' + elements.length + 'Error');
                    }
                }
            }
            return text;
        }
    }
    replaceDocumentationCall(invokation, text, result) {
        return text.slice(0, invokation.start) + result + text.slice(invokation.end + 1);
    }
    extractDocumentationCall(text) {
        const startText = '#Document:';
        let startIndex = text.indexOf(startText);
        if (startIndex === undefined) {
            return undefined;
        }
        const data = { tag: '', argumentText: '', start: startIndex, end: 0 };
        let parenthesisLevel = 0;
        for (let i = startIndex + startText.length; i < text.length; i++) {
            if (text[i] === '(' || text[i] === '#') {
                if (parenthesisLevel === 0) {
                    data.tag = text.slice(startIndex + startText.length, i);
                    data.end = i;
                }
                if (text[i] === '(') {
                    parenthesisLevel++;
                }
            }
            else if (text[i] === ')') {
                parenthesisLevel--;
                if (parenthesisLevel === 0) {
                    if (text[i + 1] === '#') {
                        data.argumentText = text.slice(data.end + 1, i);
                        data.end = i + 1;
                        return data;
                    }
                    else {
                        this.reportErrorIllformattedDocumentationCall(data.tag);
                        return undefined;
                    }
                }
            }
        }
        if (data.tag === '') {
            this.reportErrorCouldNotParseTag();
            return undefined;
        }
        else if (parenthesisLevel !== 0) {
            this.reportErrorIllformattedDocumentationCall(data.tag);
            return undefined;
        }
        return data;
    }
    reportErrorPassLimit() {
        console.error('Documenter reached pass-limit of [' + this.passLimit + ']. Documentation involving file [' + this.documentationPath + '] might not be complete.');
    }
    reportErrorUnresolvedTag(tag) {
        console.error('Could not resolve documentation tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportErrorArgumentCount(tag, parsedArguments, parameters) {
        console.error('Received [' + parsedArguments.length + '] arguments when [' + parameters.length + '] was expected. Signature: ' + parameters + ', received: ' + parsedArguments + ' in tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportErrorArgumentCountWithUnnamedArguments(tag, parsedArguments, parameters) {
        console.error('Received [' + parsedArguments.length + '] arguments when [' + parameters.length + '] was expected, not all arguments were named. Signature: ' + parameters + ', received: ' + parsedArguments + ' in tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportErrorUnmatchedUnnamedArgument(tag, parsedArgument) {
        console.error('Argument was unnamed, and could not be matched to a parameter. Argument had value: [' + parsedArgument + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportErrorParameterNotSpecified(tag, parameter) {
        console.error('Parameter was not specified: [' + parameter + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportErrorParameterNotArray(tag, parameter, parameterValue) {
        console.error('Argument expected an array, but argument was not: [' + parameter + '] with value: [' + parameterValue + '] in tag: [' + tag + '] from file: [' + this.documentationPath + ']. Arrays are comma-separated values within brackets: [x, y].');
    }
    reportErrorArrayIndexOutOfBounds(tag, parameter, requestedIndex, bounds) {
        console.error('Parameter list index [' + requestedIndex + '] was requested, but length of argument list was [' + bounds + ']. Parameter: [' + parameter + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportErrorIllformattedDocumentationCall(tag) {
        console.error('Documentation call was incorrectly formatted. Assumed tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportErrorCouldNotParseTag() {
        console.error('Tag of documentation call could not be parsed, from file: [' + this.documentationPath + '].');
    }
    reportWarningMatchedUnnamedArgument(tag, parsedArgument, matchedParameter) {
        console.warn('Argument was unnamed, but was positionally matched to parameter [' + matchedParameter + ']. Argument had value: [' + parsedArgument + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportWarningSingleUnnamedArgument(tag, parsedArgument, matchedParameter) {
        console.warn('Tag had a single unnamed argument, which was matched to parameter [' + matchedParameter + ']. Argument had value: [' + parsedArgument + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportWarningSpecifiedParameterNotPartOfSignature(tag, parameter, parameterValue) {
        console.warn('Parameter was specified, but not part of signature: [' + parameter + '] with value: [' + parameterValue + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
    reportWarningRequestedParameterNotPartOfSignature(tag, parameter) {
        console.warn('Parameter was requested, but not part of signature: [' + parameter + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].');
    }
}
exports.Documenter = Documenter;
//# sourceMappingURL=Documenter.js.map