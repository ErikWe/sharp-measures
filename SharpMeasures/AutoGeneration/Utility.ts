import { ScalarQuantity } from "./ScalarQuantity"
import { VectorQuantity } from "./VectorQuantity"

let quantityDefinitions : {
    scalars: Record<string, ScalarQuantity>,
    vectors: Record<string, VectorQuantity>
}

export type QuantityDefinitions = typeof quantityDefinitions

export const lowerCase = (text: string): string => {
    return text[0].toLowerCase() + text.slice(1)
}

export const parseUnitPlural = (singular: string, pluralCode: string): string => {
    if (pluralCode == '=') {
        return singular
    } else if (!pluralCode.includes('+')) {
        return pluralCode
    } else if (!pluralCode.includes('[')) {
        return singular + pluralCode.split('+')[1]
    } else {
        let target: string = pluralCode.split('[')[1].split(']')[0]
        if (pluralCode.split('+')[0].includes('[')) {
            return singular.replace(new RegExp(target, 'g'), target + pluralCode.split('+')[1])
        } else {
            return singular.replace(new RegExp(target), pluralCode.split('+')[1].split(' ')[0] + target)
        }
    }
}

export const ensureArray = <T>(obj: T | T[]): T[] => {
    if (Array.isArray(obj)) {
        return obj
    } else {
        return [obj]
    }
}

export const getQuantityEntry = <T extends (ScalarQuantity | VectorQuantity), K extends keyof T>(entry: K, delegate: (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity) => T[K], definitions: QuantityDefinitions, quantity: T): T[K] => {
    const value: T[K] = quantity[entry]

    if (quantity.type == 'Vector' && typeof value === 'string' && value == '[component]') {
        return delegate(definitions, definitions.scalars[getVectorComponent(definitions, quantity)])
    } else if (typeof value === 'string' && value[0] == '[') {
        if (quantity.type == 'Scalar') {
            return delegate(definitions, definitions.scalars[value.slice(1, -1)])
        } else {
            return delegate(definitions, definitions.vectors[value.slice(1, -1)])
        }
    } else {
        return quantity[entry]
    }
}

export const getVectorComponent = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): VectorQuantity['component'] => {
    if (quantity.type == 'Scalar') {
        throw new Error('Cannot retrieve vector component of scalar quantity: [' + quantity.name + '].')
    }
    
    if (quantity.component == '[name]') {
        return quantity.name
    } else if (quantity.component == '[component]') {
        throw new Error('Quantity: [' + quantity.name + '] had cyclic reference involving [component].')
    } else {
        return getQuantityEntry('component', getVectorComponent, definitions, quantity)
    }
}

export const getBaseUnits = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): (ScalarQuantity | VectorQuantity)['baseUnits'] => {
    return getQuantityEntry<ScalarQuantity | VectorQuantity, 'baseUnits'>('baseUnits', getBaseUnits, definitions, quantity)
}

export const getUnitName = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): (ScalarQuantity | VectorQuantity)['unit'] => {
    if (quantity['unit'] == '[UnitOf]') {
        return 'UnitOf' + quantity['name']
    } else {
        return getQuantityEntry('unit', getUnitName, definitions, quantity)
    }
}

export const getUnitBias = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): (ScalarQuantity | VectorQuantity)['unitBias'] => {
    return getQuantityEntry('unitBias', getUnitBias, definitions, quantity)
}

export const getNameOfVectorVersionOfScalar = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): ScalarQuantity['vector'] => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve vector version of vector quantity: [' + quantity.name + '].')
    }

    if (quantity['vector'] == '[name]') {
        return quantity['name']
    } else {
        return getQuantityEntry('vector', getNameOfVectorVersionOfScalar, definitions, quantity)
    }
}

export const getDimensionalitiesOfVector = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): VectorQuantity['dimensionalities'] => {
    if (quantity.type == 'Scalar') {
        throw new Error('Cannot retrieve dimensionalities of scalar quantity: [' + quantity.name + '].')
    }

    return getQuantityEntry('dimensionalities', getDimensionalitiesOfVector, definitions, quantity)
}

export const getVectorComponentNames = (dimensionality: number): string[] => {
    const names: string[] = ['X', 'Y', 'Z', 'W']
    return names.slice(0, dimensionality)
}

export const getInverse = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): ScalarQuantity['inverse'] => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve inverse of vector quantity: [' + quantity.name + '].')
    }

    const inverse = getQuantityEntry('inverse', getInverse, definitions, quantity)

    if (!inverse) {
        return []
    } else {
        return ensureArray(inverse)
    }
}

export const getSquare = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): ScalarQuantity['square'] => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve square of vector quantity: [' + quantity.name + '].')
    }

    const square = getQuantityEntry('square', getSquare, definitions, quantity)

    if (!square) {
        return []
    } else {
        return ensureArray(square)
    }
}

export const getCube = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): ScalarQuantity['cube'] => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve cube of vector quantity: [' + quantity.name + '].')
    }

    const cube = getQuantityEntry('cube', getCube, definitions, quantity)

    if (!cube) {
        return []
    } else {
        return ensureArray(cube)
    }
}

export const getSquareRoot = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): ScalarQuantity['squareRoot'] => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve square root of vector quantity: [' + quantity.name + '].')
    }

    const squareRoot = getQuantityEntry('squareRoot', getSquareRoot, definitions, quantity)

    if (!squareRoot) {
        return []
    } else {
        return ensureArray(squareRoot)
    }
}

export const getCubeRoot = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): ScalarQuantity['cubeRoot'] => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve cube root of vector quantity: [' + quantity.name + '].')
    }

    const cubeRoot = getQuantityEntry('cubeRoot', getCubeRoot, definitions, quantity)

    if (!cubeRoot) {
        return []
    } else {
        return ensureArray(cubeRoot)
    }
}

export const getUnits = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): (ScalarQuantity | VectorQuantity)['units'] => {
    const units = getQuantityEntry('units', getUnits, definitions, quantity)

    if (typeof units === 'string') {
        throw new Error('Could not parse unit: [' + quantity.units + '] of quantity: [' + quantity.name + '].')
    }

    for (let unit of units) {
        if (!unit.special) {
            unit.plural = parseUnitPlural(unit.singular, unit.plural)
        }
    }

    return units
}

export const getSymbol = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): string | undefined => {
    const units = getUnits(definitions, quantity)

    if (typeof units === 'string') {
        return undefined
    }

    for (let unit of units) {
        if (!unit.special && unit.SI) {
            return unit.symbol
        }
    }

    return undefined
}

export const getBases = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): (ScalarQuantity | VectorQuantity)['units'] => {
    const units = getUnits(definitions, quantity)
    
    if (typeof units === 'string') {
        return []
    }

    const bases: (ScalarQuantity | VectorQuantity)['units'] = []

    for (let unit of units) {
        if (!unit.special && (unit.base || unit.base === undefined)) {
            bases.push(unit)
        }
    }

    return bases
}

export const getConvertible = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): (ScalarQuantity | VectorQuantity)['convertible'] => {
    const convertible = getQuantityEntry('convertible', getConvertible, definitions, quantity)

    if (!convertible) {
        return []
    } else {
        return ensureArray(convertible)
    }
}

export const createUnitListTexts = (quantity: ScalarQuantity | VectorQuantity) : { singular: string, plural: string } => {
    const units = quantity.units
    
    if (typeof units === 'string') {
        throw new Error('Could not parse bases of quantity: [' + quantity.name + '], units was not parsed correctly.')
    }

    let singularUnitList = ''
    let pluralUnitList = ''

    for (let unit of units) {
        if (!unit.special) {
            singularUnitList += unit.singular + ', '
            pluralUnitList += unit.plural + ', '
        }
    }

    return { singular: '[' + singularUnitList.slice(0, -2) + ']', plural: '[' + pluralUnitList.slice(0, -2) + ']' }
}

export const insertAppropriateNewlines = (text: string): string => {
    let rebuilt: string = ''

    for (let line of text.split('\n')) {
        if (line.includes('#newline#')) {
            const indent: string = line.match(new RegExp('[ ]*'))?.[0] ?? ''
            const comment: boolean = line.trim() == '///'
            let length: number = 0
            const components: string[] = line.split('#newline#')

            for (let component of components) {
                if (length > 0 && length + component.length > 175) {
                    length = 0

                    rebuilt += '\n' + indent
                    if (comment) {
                        rebuilt += '/// '
                    } else {
                        rebuilt += '\t'
                    }
                }

                rebuilt += component
                length += component.length
            }
            rebuilt += '\n'
        } else {
            rebuilt += line + '\n'
        }
    }

    return rebuilt
}

export const removeConsecutiveNewlines = (text: string): string => {
    let rebuilt: string = ''
    let previousWasEmpty: boolean = true

    for (let line of text.split('\n')) {
        if (line.trim() == '' || line.trim() == '\r') {
            if (!previousWasEmpty) {
                previousWasEmpty = true
                rebuilt += line + '\n'
            }
        } else {
            previousWasEmpty = false
            rebuilt += line + '\n'
        }
    }

    if (previousWasEmpty) {
        rebuilt = rebuilt.slice(0, -1)
    }

    return rebuilt.slice(0, -1)
}

export const normalizeLineEndings = (text: string): string => {
    text = text.replace(new RegExp('(?<!\\r)\\n', 'g'), '\r\n')
    text = text.replace(new RegExp('\\r(?!\\n)', 'g'), '\r\n')
    return text
}