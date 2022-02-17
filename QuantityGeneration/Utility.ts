import { ScalarQuantity } from './ScalarQuantity'
import { VectorQuantity } from './VectorQuantity'
import { Unit } from './Unit'

let quantityDefinitions : {
    scalars: Record<string, ScalarQuantity>,
    vectors: Record<string, VectorQuantity>,
    units: Record<string, Unit>
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

export const getVectorComponent = (definitions: QuantityDefinitions, quantity: VectorQuantity): ScalarQuantity => {
    if (quantity.component === '[name]') {
        return definitions.scalars[quantity.name]
    } else if (quantity.component === '[component]') {
        throw new Error('Quantity: [' + quantity.name + '] had cyclic reference involving [component].')
    } else if (quantity.component[0] === '[') {
        return getVectorComponent(definitions, definitions.vectors[quantity.component.slice(1, -1)])
    } else {
        return definitions.scalars[quantity.component]
    }
}

export const getUnit = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): Unit => {
    if (quantity.unit === '[name]') {
        return definitions.units[quantity.name]
    } else if (quantity.type === 'Vector' && quantity.unit === '[component]') {
        return getUnit(definitions, getVectorComponent(definitions, quantity))
    } else if (quantity.unit[0] === '[') {
        if (quantity.type === 'Vector') {
            return getUnit(definitions, definitions.vectors[quantity.unit.slice(1, -1)]) 
        } else {
            return getUnit(definitions, definitions.scalars[quantity.unit.slice(1, -1)]) 
        }
    } else {
        return definitions.units[quantity.unit]
    }
}

export const getVectorVersionOfScalar = (definitions: QuantityDefinitions, quantity: ScalarQuantity): VectorQuantity | undefined => {
    if (quantity.vector === false) {
        return undefined
    } else if (quantity.vector === '[name]') {
        return definitions.vectors[quantity.name]
    } else {
        return definitions.vectors[quantity.vector]
    }
}

export const getVectorComponentNames = (dimensionality: number): string[] => {
    const names: string[] = ['X', 'Y', 'Z', 'W']
    return names.slice(0, dimensionality)
}

export const getUnits = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): Unit['units'] => {
    const unit: Unit = getUnit(definitions, quantity)

    const includedUnits: Unit['units'] = []
    for (let namedUnit of unit.units) {
        if (!namedUnit.special) {
            if (!isUnitExcluded(quantity, namedUnit, quantity.excludeUnits) && !isUnitUnincluded(quantity, namedUnit, quantity.includeUnits)) {
                includedUnits.push(namedUnit)
            }
        }
    }

    return includedUnits
}

export const getBases = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): Unit['units'] => {
    const units: Unit['units'] = getUnits(definitions, quantity)

    const includedUnits: Unit['units'] = []
    for (let unit of units) {
        if (!unit.special) {
            if (!isUnitExcluded(quantity, unit, quantity.excludeBases) && !isUnitUnincluded(quantity, unit, quantity.includeBases)) {
                includedUnits.push(unit)
            }
        }
    }

    return includedUnits
}

const isUnitExcluded = (quantity: ScalarQuantity | VectorQuantity, unit: Unit['units'][number], list: string[] | undefined): boolean => {
    return (unit.special !== true && list !== undefined && list.includes(unit.name))
}

const isUnitUnincluded = (quantity: ScalarQuantity | VectorQuantity, unit: Unit['units'][number], list: string[] | undefined): boolean => {
    return (unit.special !== true && list !== undefined && !list.includes(unit.name))
}

export const getSIUnit = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): Unit['units'][number] | undefined => {
    const units: Unit['units'] = getUnits(definitions, quantity)

    for (let unit of units) {
        if (!unit.special && unit.SI) {
            return unit
        }
    }

    return undefined
}

export const getDefaultUnit = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): Unit['units'][number] | undefined => {
    if (quantity.defaultUnit === undefined) {
        return getSIUnit(definitions, quantity)
    } else if (quantity.type === 'Vector' && quantity.defaultUnit === '[component]') {
        return getDefaultUnit(definitions, getVectorComponent(definitions, quantity))
    } else {
        const units: Unit['units'] = getUnits(definitions, quantity)

        for (let unit of units) {
            if (!unit.special && unit.name === quantity.defaultUnit) {
                return unit
            }
        }
    }

    return undefined
}

export const getConvertible = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): ScalarQuantity[] | VectorQuantity[] => {
    if (quantity.convertible === undefined) {
        return []
    }

    if (quantity.type === 'Scalar') {
        const convertibles: ScalarQuantity[] = []
        for (let convertible of quantity.convertible) {
            convertibles.push(definitions.scalars[convertible])
        }
        return convertibles
    } else {
        const convertibles: VectorQuantity[] = []
        for (let convertible of quantity.convertible) {
            convertibles.push(definitions.vectors[convertible])
        }
        return convertibles
    }
}

export const getUnitQuantity = (definitions: QuantityDefinitions, unit: Unit): ScalarQuantity => {
    if (unit.quantity === '[name]') {
        return definitions.scalars[unit.name]
    } else {
        return definitions.scalars[unit.quantity]
    }
}

export const composeUnitsNameList = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): { singular: string, plural: string } => {
    const units: Unit['units'] = getUnits(definitions, quantity)

    let singular: string = ''
    let plural: string = ''

    for (let unit of units) {
        if (!unit.special) {
            singular += unit.name + ', '
            plural += parseUnitPlural(unit.name, unit.plural) + ', '
        }
    }

    return { singular: '[' + singular.slice(0, -2) + ']', plural: '[' + plural.slice(0, -2) + ']' }
}

export const composeBasesNameList = (definitions: QuantityDefinitions, quantity: ScalarQuantity | VectorQuantity): { singular: string, plural: string } => {
    const units: Unit['units'] = getBases(definitions, quantity)

    let singular: string = ''
    let plural: string = ''

    for (let unit of units) {
        if (!unit.special) {
            singular += unit.name + ', '
            plural += parseUnitPlural(unit.name, unit.plural) + ', '
        }
    }

    return { singular: '[' + singular.slice(0, -2) + ']', plural: '[' + plural.slice(0, -2) + ']' }
}

export const insertAppropriateNewlines = (text: string, characterLimit: number): string => {
    let rebuilt: string = ''

    for (let line of text.split('\n')) {
        if (line.includes('#newline#')) {
            rebuilt += replaceNewLineIfNecessary(line, characterLimit) + '\n'
        } else {
            rebuilt += line + '\n'
        }
    }

    return rebuilt
}

const replaceNewLineIfNecessary = (line: string, characterLimit: number): string => {
    const indent: string = line.match(/[ ]*/)?.[0] ?? ''
    const comment: boolean = line.trim().startsWith('///')
    let length: number = 0
    const components: string[] = line.split('#newline#')

    let rebuilt: string = ''

    for (let component of components) {
        if (length > 0 && length + component.length > characterLimit) {
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
    text = text.replace(/(?<!\r)\n/g, '\r\n')
    text = text.replace(/\r(?!\n)/g, '\r\n')
    return text
}