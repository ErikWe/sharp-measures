import fsp from 'fs/promises'
import path from 'path'

export const readTag = async (tag: string, fileName: string, searchedFiles: string[] | undefined = undefined): Promise<{ content: string, parameters: string[] } | false> => {
    if (searchedFiles === undefined) {
        searchedFiles = []
    }

    const text: string | undefined = await readFile(fileName, 'utf-8')

    if (text === undefined) {
        reportErrorFileNotFound(fileName)
        return false
    }

    const matched: { content: string, parameters: string[] } | false = extractTag(tag, text)
    searchedFiles.push(fileName)

    if (matched === false) {
        return readTagFromExtension(tag, fileName, searchedFiles)
    } else {
        return matched
    }
}

const readTagFromExtension = async (tag: string, fileName: string, searchedFiles: string[]): Promise<{ content: string, parameters: string[] } | false> => {
    const regex: RegExp = /#Extends:([A-Za-z0-9_\-.]+?)(?:\r\n|\n|#)\s\g/

    const text: string = await fsp.readFile(fileName, { encoding: 'utf-8' })
    const extending: IterableIterator<RegExpMatchArray> = text.matchAll(regex)

    if (extending === null) {
        return false
    }

    for (let extension of extending) {
        const nextFileName: string = path.dirname(fileName) + '\\' + extension[1]
        if (!searchedFiles.includes(nextFileName)) {
            const matched: { content: string, parameters: string[] } | false = await readTag(tag, nextFileName, searchedFiles)
            if (matched !== false) {
                return matched
            }
        }
    }

    return false
}

const extractTag = (tag: string, text: string): { content: string, parameters: string[] } | false => {
    const regex: RegExp = new RegExp('#Define:' + tag + '(\\r\\n|\\n|#|\\([A-z0-9, _\\-]*\\))(?:#?)(?:\\n|\\r\\n|\\s*?)([^]+?)(?:\\r\\n|\\n|\\s*)#\\/', 's')
    const regexResult: RegExpMatchArray | null = text.match(regex)

    if (regexResult === null) {
        return false
    }

    const content: string = regexResult[2]
    const parameters: string[] = regexResult[1].includes('(') ? regexResult[1].slice(1, -1).split(',') : []

    for (let i = 0; i < parameters.length; i++) {
        parameters[i] = parameters[i].trim()
    }

    return { content: content, parameters: parameters }
}

const readFile = async (fileName: string, encoding: string): Promise<string | undefined> => {
    if (await fsp.stat(fileName).catch(() => false)) {
        return fsp.readFile(fileName, { encoding: 'utf-8' })
    } else {
        return undefined
    }
}

const reportErrorFileNotFound = (fileName: string): void => {
    console.error('File was not found: [' + fileName + '].')
}