import { readTag } from "./DocumentationReader"

export class Documenter {

    public static async document(text: string, documentationPath: string): Promise<string> {
        return await new Documenter(documentationPath, 50).run(text)
    }

    private constructor(private documentationPath: string, private passLimit: number = 50) {}

    private async run(text: string): Promise<string> {
        let i: number = 0
        while (true) {
            const update: { text: string, modified: boolean } = await this.documentationPass(text)
            if (update.modified) {
                text = update.text
            } else {
                return text
            }

            if (++i >= this.passLimit) {
                this.reportErrorPassLimit()
                return text
            }
        }
    }

    private async documentationPass(text: string): Promise<{ text: string, modified: boolean }> {
        let rebuilt: string = ''
        let modified: boolean = false

        for (let line of text.split('\n')) {
            if (line.includes('#Document:')) {
                const indent: string = line.match(new RegExp('[ ]*'))?.[0] ?? ''
    
                modified = true
                for (let rebuiltLine of await this.produceDocumentationLines(line)) {
                    rebuilt += indent + rebuiltLine.trim() + '\n'
                }
            } else {
                rebuilt += line + '\n'
            }
        }

        return { text: rebuilt.slice(0, -1), modified: modified }
    }

    private async produceDocumentationLines(line: string): Promise<string[]> {
        const invokation: { tag: string, argumentText: string, start: number, end: number } | undefined = this.extractDocumentationCall(line)

        if (invokation === undefined) {
            return [line]
        }

        const parsedArguments: string[] = this.extractArguments(invokation.argumentText)

        const parameterValues: Record<string, string> = {}

        let anyUnnamed: boolean = false
        for (let parsedArgument of parsedArguments) {
            if (parsedArgument.includes('=')) {
                const parameter = parsedArgument.split('=')[0].trim()
                const value = parsedArgument.split('=')[1].trim()
    
                parameterValues[parameter] = value
            } else {
                anyUnnamed = true
            }
        }

        const tagData: { content: string, parameters: string[] } | false = await readTag(invokation.tag, this.documentationPath)

        if (!tagData) {
            this.reportErrorUnresolvedTag(invokation.tag)
            return this.replaceDocumentationCall(invokation, line, 'UnresolvedTagError').split('\n')
        }

        if (parsedArguments.length < tagData.parameters.length) {
            this.reportErrorArgumentCount(invokation.tag, parsedArguments, tagData.parameters)
            return this.replaceDocumentationCall(invokation, line, 'ArgumentCountError').split('\n')
        }

        if (anyUnnamed) {
            if (!this.rescueUnnamedArguments(invokation.tag, parsedArguments, tagData.parameters, parameterValues)) {
                return this.replaceDocumentationCall(invokation, line, 'ArgumentMatchingError').split('\n')
            }
        }

        await Promise.all(Object.keys(parameterValues).map(async (parameter) => {
            parameterValues[parameter] = await Documenter.document(parameterValues[parameter], this.documentationPath)
        }))

        tagData.content = this.injectParameters(invokation.tag, tagData.content, tagData.parameters, parameterValues)

        for (let parameter of Object.keys(parameterValues)) {
            if (!tagData.parameters.includes(parameter)) {
                this.reportWarningSpecifiedParameterNotPartOfSignature(invokation.tag, parameter, parameterValues[parameter])
            }
        }

        if (tagData.content.includes('#Param:')) {
            let requestedParameters: IterableIterator<RegExpMatchArray> = tagData.content.matchAll(new RegExp('#Param:([A-z0-9_\-]*)(\\[(%?)([0-9]+?)\\]?)#', 'g'))
            for (let requestedParameter of requestedParameters) {
                this.reportWarningRequestedParameterNotPartOfSignature(invokation.tag, requestedParameter[1])
            }
        }

        return this.replaceDocumentationCall(invokation, line, tagData.content).split('\n')
    }

    private extractArguments(argumentText: string): string[] {
        if (argumentText.trim() === '') {
            return []
        }

        const splitIndices: number[] = []
        let parenthesisLevel: number = 0
        let bracketLevel: number = 0

        for (let i = 0; i < argumentText.length; i++) {
            const character: string = argumentText[i];
            if (character === '(') {
                parenthesisLevel++
            } else if (character === ')') {
                parenthesisLevel--
            } else if (character === '[') {
                bracketLevel++
            } else if (character === ']') {
                bracketLevel--
            } else if (parenthesisLevel === 0 && bracketLevel === 0 && character === ',') {
                splitIndices.push(i)
            }
        }

        const parsedArguments: string[] = []

        for (let i = 0; i < splitIndices.length; i++) {
            if (i === 0) {
                parsedArguments.push(argumentText.slice(0, splitIndices[i]).trim())
            } else {
                parsedArguments.push(argumentText.slice(splitIndices[i - 1] + 1, splitIndices[i]).trim())
            }
        }

        if (splitIndices.length === 0) {
            parsedArguments.push(argumentText)
        } else {
            parsedArguments.push(argumentText.slice(splitIndices.pop()! + 1).trim())
        }
        
        return parsedArguments
    }

    private rescueUnnamedArguments(tag: string, parsedArguments: string[], parameters: string[], parameterValues: Record<string, string>): boolean {
        if (Object.keys(parameterValues).length > 0) {
            return this.rescueMixedUnnamedArguments(tag, parsedArguments, parameters, parameterValues)
        } else {
            return this.rescueUnmixedUnnamedArguments(tag, parsedArguments, parameters, parameterValues)
        }
    }

    private rescueMixedUnnamedArguments(tag: string, parsedArguments: string[], parameters: string[], parameterValues: Record<string, string>): boolean {
        if (parsedArguments.length != parameters.length) {
            this.reportErrorArgumentCountWithUnnamedArguments(tag, parsedArguments, parameters)
            return false
        } else {
            if (Object.keys(parameterValues).length == parameters.length - 1) {
                return this.rescueSingleMixedUnnamedArgument(tag, parsedArguments, parameters, parameterValues)
            } else {
                for (let i = 0; i < parsedArguments.length; i++) {
                    if (!parsedArguments[i].includes('=')) {
                        if (Object.keys(parameterValues).includes(parameters[i])) {
                            this.reportErrorUnmatchedUnnamedArgument(tag, parsedArguments[i])
                            return false
                        } else {
                            parameterValues[parameters[i]] = parsedArguments[i]
                            this.reportWarningMatchedUnnamedArgument(tag, parsedArguments[i], parameters[i])
                        }
                    }
                }
                return true
            }
        }
    }

    private rescueUnmixedUnnamedArguments(tag: string, parsedArguments: string[], parameters: string[], parameterValues: Record<string, string>): boolean {
        if (parsedArguments.length != parameters.length) {
            this.reportErrorArgumentCountWithUnnamedArguments(tag, parsedArguments, parameters)
            return false
        } else {
            for (let i = 0; i < parsedArguments.length; i++) {
                parameterValues[parameters[i]] = parsedArguments[i]
            }
            return true
        }
    }

    private rescueSingleMixedUnnamedArgument(tag: string, parsedArguments: string[], parameters: string[], parameterValues: Record<string, string>): boolean {
        const getUnnamedArgument = (items: string[]): string | undefined => {
            for (let item of items) {
                if (!item.includes('=')) {
                    return item
                }
            }
            return undefined
        }

        const unnamedArgument: string | undefined = getUnnamedArgument(parsedArguments)
        if (unnamedArgument === undefined) {
            return false
        }

        for (let parameter of parameters) {
            if (!Object.keys(parameterValues).includes(parameter)) {
                parameterValues[parameter] = unnamedArgument
                this.reportWarningSingleUnnamedArgument(tag, unnamedArgument, parameter)
                return true
            }
        }

        return false
    }

    private injectParameters(tag: string, text: string, parameters: string[], parameterValues: Record<string, string>): string {
        for (let parameter of parameters) {
            if (!Object.keys(parameterValues).includes(parameter)) {
                this.reportErrorParameterNotSpecified(tag, parameter)
                text = text.replace(new RegExp('#Param:' + parameter + '#', 'g'), 'ParameterNotSpecifiedError')
            } else {
                text = text.replace(new RegExp('#Param:' + parameter + '#', 'g'), parameterValues[parameter])
                
                let arrayAccess: IterableIterator<RegExpMatchArray> = text.matchAll(new RegExp('#Param:' + parameter + '\\[(%?)([0-9]+?)\\]#', 'g'))
                text = this.injectArrayParameter(tag, text, parameter, parameterValues[parameter], arrayAccess)
            }
        }

        return text
    }

    private injectArrayParameter(tag: string, text: string, parameter: string, parameterValue: string, arrayAccess: IterableIterator<RegExpMatchArray>): string {
        if ((parameterValue.length == 0 || parameterValue[0] != '[') && arrayAccess.next().done === false) {
            this.reportErrorParameterNotArray(tag, parameter, parameterValue)
            return text.replace(new RegExp('#Param:' + parameter + '\\[(%?)([0-9]+?)\\]#', 'g'), 'ParameterNotArrayError')
        } else {
            const elements: string[] = parameterValue.slice(1, -1).split(',').map((element) => element.trim())
            for (let access of arrayAccess) {
                let index: number = Number(access[2])

                if (access[1] == '%') {
                    text = text.replace(new RegExp('#Param:' + parameter + '\\[%' + access[2] + '\\]#', 'g'), elements[index % elements.length])
                } else {
                    if (index < elements.length) {
                        text = text.replace(new RegExp('#Param:' + parameter + '\\[' + access[2] + '\\]#', 'g'), elements[index])
                    } else {
                        this.reportErrorArrayIndexOutOfBounds(tag, parameter, index, elements.length)
                        text = text.replace(new RegExp('#Param:' + parameter + '\\[([0-9]+?)\\]#', 'g'), 'ParameterIndex$1OutOfBounds' + elements.length + 'Error')
                    }
                }
            }

            return text
        }
    }

    private replaceDocumentationCall(invokation: { start: number, end: number }, text: string, result: string): string {
        let part1 = text.slice(0, invokation.start)
        let part2 = text.slice(invokation.end)
        return text.slice(0, invokation.start) + result + text.slice(invokation.end + 1)
    }

    private extractDocumentationCall(text: string): { tag: string, argumentText: string, start: number, end: number } | undefined {
        const startText: string = '#Document:'

        let startIndex: number = text.indexOf(startText)

        if (startIndex === undefined) {
            return undefined
        }

        const data: { tag: string, argumentText: string, start: number, end: number } = { tag: '', argumentText: '', start: startIndex, end: 0 }

        let parenthesisLevel: number = 0

        for (let i = startIndex + startText.length; i < text.length; i++) {
            if (text[i] === '(' || text[i] === '#') {
                if (parenthesisLevel === 0) {
                    data.tag = text.slice(startIndex + startText.length, i)
                    data.end = i
                }
                if (text[i] === '(') {
                    parenthesisLevel++
                }
            } else if (text[i] === ')') {
                parenthesisLevel--
                if (parenthesisLevel === 0) {
                    if (text[i + 1] === '#') {
                        data.argumentText = text.slice(data.end + 1, i)
                        data.end = i + 1
                        return data
                    } else {
                        this.reportErrorIllformattedDocumentationCall(data.tag)
                        return undefined
                    }
                }
            }
        }

        if (data.tag === '') {
            this.reportErrorCouldNotParseTag()
            return undefined
        } else if (parenthesisLevel !== 0) {
            this.reportErrorIllformattedDocumentationCall(data.tag)
            return undefined
        }

        return data
    }

    private reportErrorPassLimit(): void {
        console.error('Documenter reached pass-limit of [' + this.passLimit + ']. Documentation involving file [' + this.documentationPath + '] might not be complete.')
    }

    private reportErrorUnresolvedTag(tag: string): void {
        console.error('Could not resolve documentation tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }

    private reportErrorArgumentCount(tag: string, parsedArguments: string[], parameters: string[]): void {
        console.error('Received [' + parsedArguments.length + '] arguments when [' + parameters.length + '] was expected. Signature: ' + parameters + ', received: ' + parsedArguments + ' in tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }

    private reportErrorArgumentCountWithUnnamedArguments(tag: string, parsedArguments: string[], parameters: string[]): void {
        console.error('Received [' + parsedArguments.length + '] arguments when [' + parameters.length + '] was expected, not all arguments were named. Signature: ' + parameters + ', received: ' + parsedArguments + ' in tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }

    private reportErrorUnmatchedUnnamedArgument(tag: string, parsedArgument: string): void {
        console.error('Argument was unnamed, and could not be matched to a parameter. Argument had value: [' + parsedArgument + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }
    
    private reportErrorParameterNotSpecified(tag: string, parameter: string): void {
        console.error('Parameter was not specified: [' + parameter + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }

    private reportErrorParameterNotArray(tag: string, parameter: string, parameterValue: string): void {
        console.error('Argument expected an array, but argument was not: [' + parameter + '] with value: [' + parameterValue + '] in tag: [' + tag + '] from file: [' + this.documentationPath + ']. Arrays are comma-separated values within brackets: [x, y].')
    }

    private reportErrorArrayIndexOutOfBounds(tag: string, parameter: string, requestedIndex: number, bounds: number): void {
        console.error('Parameter list index [' + requestedIndex + '] was requested, but length of argument list was [' + bounds + ']. Parameter: [' + parameter + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }

    private reportErrorIllformattedDocumentationCall(tag: string) {
        console.error('Documentation call was incorrectly formatted. Assumed tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }

    private reportErrorCouldNotParseTag() {
        console.error('Tag of documentation call could not be parsed, from file: [' + this.documentationPath + '].')
    }

    private reportWarningMatchedUnnamedArgument(tag: string, parsedArgument: string, matchedParameter: string): void {
        console.warn('Argument was unnamed, but was positionally matched to parameter [' + matchedParameter + ']. Argument had value: [' + parsedArgument + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }

    private reportWarningSingleUnnamedArgument(tag: string, parsedArgument: string, matchedParameter: string): void {
        console.warn('Tag had a single unnamed argument, which was matched to parameter [' + matchedParameter + ']. Argument had value: [' + parsedArgument + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }

    private reportWarningSpecifiedParameterNotPartOfSignature(tag: string, parameter: string, parameterValue: string): void {
        console.warn('Parameter was specified, but not part of signature: [' + parameter + '] with value: [' + parameterValue + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }

    private reportWarningRequestedParameterNotPartOfSignature(tag: string, parameter: string) {
        console.warn('Parameter was requested, but not part of signature: [' + parameter + '] in tag: [' + tag + '] from file: [' + this.documentationPath + '].')
    }
}