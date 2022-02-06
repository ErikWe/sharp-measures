"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.normalizeLineEndings = exports.removeConsecutiveNewlines = exports.insertAppropriateNewlines = exports.createUnitListTexts = exports.getConvertible = exports.getBases = exports.getSymbol = exports.getUnits = exports.getCubeRoot = exports.getSquareRoot = exports.getCube = exports.getSquare = exports.getInverse = exports.getVectorComponentNames = exports.getDimensionalitiesOfVector = exports.getNameOfVectorVersionOfScalar = exports.getUnitBias = exports.getUnitName = exports.getBaseUnits = exports.getVectorComponent = exports.getQuantityEntry = exports.ensureArray = exports.parseUnitPlural = exports.lowerCase = void 0;
let quantityDefinitions;
const lowerCase = (text) => {
    return text[0].toLowerCase() + text.slice(1);
};
exports.lowerCase = lowerCase;
const parseUnitPlural = (singular, pluralCode) => {
    if (pluralCode == '=') {
        return singular;
    }
    else if (!pluralCode.includes('+')) {
        return pluralCode;
    }
    else if (!pluralCode.includes('[')) {
        return singular + pluralCode.split('+')[1];
    }
    else {
        let target = pluralCode.split('[')[1].split(']')[0];
        if (pluralCode.split('+')[0].includes('[')) {
            return singular.replace(new RegExp(target, 'g'), target + pluralCode.split('+')[1]);
        }
        else {
            return singular.replace(new RegExp(target), pluralCode.split('+')[1].split(' ')[0] + target);
        }
    }
};
exports.parseUnitPlural = parseUnitPlural;
const ensureArray = (obj) => {
    if (Array.isArray(obj)) {
        return obj;
    }
    else {
        return [obj];
    }
};
exports.ensureArray = ensureArray;
const getQuantityEntry = (entry, delegate, definitions, quantity) => {
    const value = quantity[entry];
    if (quantity.type == 'Vector' && typeof value === 'string' && value == '[component]') {
        return delegate(definitions, definitions.scalars[(0, exports.getVectorComponent)(definitions, quantity)]);
    }
    else if (typeof value === 'string' && value[0] == '[') {
        if (quantity.type == 'Scalar') {
            return delegate(definitions, definitions.scalars[value.slice(1, -1)]);
        }
        else {
            return delegate(definitions, definitions.vectors[value.slice(1, -1)]);
        }
    }
    else {
        return quantity[entry];
    }
};
exports.getQuantityEntry = getQuantityEntry;
const getVectorComponent = (definitions, quantity) => {
    if (quantity.type == 'Scalar') {
        throw new Error('Cannot retrieve vector component of scalar quantity: [' + quantity.name + '].');
    }
    if (quantity.component == '[name]') {
        return quantity.name;
    }
    else if (quantity.component == '[component]') {
        throw new Error('Quantity: [' + quantity.name + '] had cyclic reference involving [component].');
    }
    else {
        return (0, exports.getQuantityEntry)('component', exports.getVectorComponent, definitions, quantity);
    }
};
exports.getVectorComponent = getVectorComponent;
const getBaseUnits = (definitions, quantity) => {
    return (0, exports.getQuantityEntry)('baseUnits', exports.getBaseUnits, definitions, quantity);
};
exports.getBaseUnits = getBaseUnits;
const getUnitName = (definitions, quantity) => {
    if (quantity['unit'] == '[UnitOf]') {
        return 'UnitOf' + quantity['name'];
    }
    else {
        return (0, exports.getQuantityEntry)('unit', exports.getUnitName, definitions, quantity);
    }
};
exports.getUnitName = getUnitName;
const getUnitBias = (definitions, quantity) => {
    return (0, exports.getQuantityEntry)('unitBias', exports.getUnitBias, definitions, quantity);
};
exports.getUnitBias = getUnitBias;
const getNameOfVectorVersionOfScalar = (definitions, quantity) => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve vector version of vector quantity: [' + quantity.name + '].');
    }
    if (quantity['vector'] == '[name]') {
        return quantity['name'];
    }
    else {
        return (0, exports.getQuantityEntry)('vector', exports.getNameOfVectorVersionOfScalar, definitions, quantity);
    }
};
exports.getNameOfVectorVersionOfScalar = getNameOfVectorVersionOfScalar;
const getDimensionalitiesOfVector = (definitions, quantity) => {
    if (quantity.type == 'Scalar') {
        throw new Error('Cannot retrieve dimensionalities of scalar quantity: [' + quantity.name + '].');
    }
    return (0, exports.getQuantityEntry)('dimensionalities', exports.getDimensionalitiesOfVector, definitions, quantity);
};
exports.getDimensionalitiesOfVector = getDimensionalitiesOfVector;
const getVectorComponentNames = (dimensionality) => {
    const names = ['X', 'Y', 'Z', 'W'];
    return names.slice(0, dimensionality);
};
exports.getVectorComponentNames = getVectorComponentNames;
const getInverse = (definitions, quantity) => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve inverse of vector quantity: [' + quantity.name + '].');
    }
    const inverse = (0, exports.getQuantityEntry)('inverse', exports.getInverse, definitions, quantity);
    if (!inverse) {
        return [];
    }
    else {
        return (0, exports.ensureArray)(inverse);
    }
};
exports.getInverse = getInverse;
const getSquare = (definitions, quantity) => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve square of vector quantity: [' + quantity.name + '].');
    }
    const square = (0, exports.getQuantityEntry)('square', exports.getSquare, definitions, quantity);
    if (!square) {
        return [];
    }
    else {
        return (0, exports.ensureArray)(square);
    }
};
exports.getSquare = getSquare;
const getCube = (definitions, quantity) => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve cube of vector quantity: [' + quantity.name + '].');
    }
    const cube = (0, exports.getQuantityEntry)('cube', exports.getCube, definitions, quantity);
    if (!cube) {
        return [];
    }
    else {
        return (0, exports.ensureArray)(cube);
    }
};
exports.getCube = getCube;
const getSquareRoot = (definitions, quantity) => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve square root of vector quantity: [' + quantity.name + '].');
    }
    const squareRoot = (0, exports.getQuantityEntry)('squareRoot', exports.getSquareRoot, definitions, quantity);
    if (!squareRoot) {
        return [];
    }
    else {
        return (0, exports.ensureArray)(squareRoot);
    }
};
exports.getSquareRoot = getSquareRoot;
const getCubeRoot = (definitions, quantity) => {
    if (quantity.type == 'Vector') {
        throw new Error('Cannot retrieve cube root of vector quantity: [' + quantity.name + '].');
    }
    const cubeRoot = (0, exports.getQuantityEntry)('cubeRoot', exports.getCubeRoot, definitions, quantity);
    if (!cubeRoot) {
        return [];
    }
    else {
        return (0, exports.ensureArray)(cubeRoot);
    }
};
exports.getCubeRoot = getCubeRoot;
const getUnits = (definitions, quantity) => {
    const units = (0, exports.getQuantityEntry)('units', exports.getUnits, definitions, quantity);
    if (typeof units === 'string') {
        throw new Error('Could not parse unit: [' + quantity.units + '] of quantity: [' + quantity.name + '].');
    }
    for (let unit of units) {
        if (!unit.special) {
            unit.plural = (0, exports.parseUnitPlural)(unit.singular, unit.plural);
        }
    }
    return units;
};
exports.getUnits = getUnits;
const getSymbol = (definitions, quantity) => {
    const units = (0, exports.getUnits)(definitions, quantity);
    if (typeof units === 'string') {
        return undefined;
    }
    for (let unit of units) {
        if (!unit.special && unit.SI) {
            return unit.symbol;
        }
    }
    return undefined;
};
exports.getSymbol = getSymbol;
const getBases = (definitions, quantity) => {
    const units = (0, exports.getUnits)(definitions, quantity);
    if (typeof units === 'string') {
        return [];
    }
    const bases = [];
    for (let unit of units) {
        if (!unit.special && (unit.base || unit.base === undefined)) {
            bases.push(unit);
        }
    }
    return bases;
};
exports.getBases = getBases;
const getConvertible = (definitions, quantity) => {
    const convertible = (0, exports.getQuantityEntry)('convertible', exports.getConvertible, definitions, quantity);
    if (!convertible) {
        return [];
    }
    else {
        return (0, exports.ensureArray)(convertible);
    }
};
exports.getConvertible = getConvertible;
const createUnitListTexts = (quantity) => {
    const units = quantity.units;
    if (typeof units === 'string') {
        throw new Error('Could not parse bases of quantity: [' + quantity.name + '], units was not parsed correctly.');
    }
    let singularUnitList = '';
    let pluralUnitList = '';
    for (let unit of units) {
        if (!unit.special) {
            singularUnitList += unit.singular + ', ';
            pluralUnitList += unit.plural + ', ';
        }
    }
    return { singular: '[' + singularUnitList.slice(0, -2) + ']', plural: '[' + pluralUnitList.slice(0, -2) + ']' };
};
exports.createUnitListTexts = createUnitListTexts;
const insertAppropriateNewlines = (text, characterLimit) => {
    let rebuilt = '';
    for (let line of text.split('\n')) {
        if (line.includes('#newline#')) {
            rebuilt += replaceNewLineIfNecessary(line, characterLimit) + '\n';
        }
        else {
            rebuilt += line + '\n';
        }
    }
    return rebuilt;
};
exports.insertAppropriateNewlines = insertAppropriateNewlines;
const replaceNewLineIfNecessary = (line, characterLimit) => {
    var _a, _b;
    const indent = (_b = (_a = line.match(/[ ]*/)) === null || _a === void 0 ? void 0 : _a[0]) !== null && _b !== void 0 ? _b : '';
    const comment = line.trim().startsWith('///');
    let length = 0;
    const components = line.split('#newline#');
    let rebuilt = '';
    for (let component of components) {
        if (length > 0 && length + component.length > characterLimit) {
            length = 0;
            rebuilt += '\n' + indent;
            if (comment) {
                rebuilt += '/// ';
            }
            else {
                rebuilt += '\t';
            }
        }
        rebuilt += component;
        length += component.length;
    }
    return rebuilt;
};
const removeConsecutiveNewlines = (text) => {
    let rebuilt = '';
    let previousWasEmpty = true;
    for (let line of text.split('\n')) {
        if (line.trim() == '' || line.trim() == '\r') {
            if (!previousWasEmpty) {
                previousWasEmpty = true;
                rebuilt += line + '\n';
            }
        }
        else {
            previousWasEmpty = false;
            rebuilt += line + '\n';
        }
    }
    if (previousWasEmpty) {
        rebuilt = rebuilt.slice(0, -1);
    }
    return rebuilt.slice(0, -1);
};
exports.removeConsecutiveNewlines = removeConsecutiveNewlines;
const normalizeLineEndings = (text) => {
    text = text.replace(/(?<!\r)\n/g, '\r\n');
    text = text.replace(/\r(?!\n)/g, '\r\n');
    return text;
};
exports.normalizeLineEndings = normalizeLineEndings;
//# sourceMappingURL=Utility.js.map