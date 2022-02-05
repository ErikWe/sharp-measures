import fsp from 'fs/promises'
import { Documenter } from './Documenter'
import { DefinitionReader } from './DefinitionReader'
import { ScalarQuantity } from './ScalarQuantity'
import { TemplateReader } from './TemplateReader'
import { createUnitListTexts, getBases, getBaseUnits, getConvertible, getCube, getCubeRoot, getDimensionalitiesOfVector, getInverse, getNameOfVectorVersionOfScalar,
    getSquare, getSquareRoot, getSymbol, getUnitBias, getUnitName, getUnits, getVectorComponentNames, insertAppropriateNewlines, lowerCase, normalizeLineEndings,
    removeConsecutiveNewlines, parseUnitPlural } from './Utility'

export class ScalarGenerator {

    public constructor(private destination: string, private documentationDirectory: string,
        private definitionReader: DefinitionReader, private templateReader: TemplateReader) {}

    public async generate(): Promise<void> {
        const keys: string[] = Object.keys(this.definitionReader.definitions.scalars)

        await Promise.all(keys.map(async (key: string) => {
            this.generateScalar(this.definitionReader.definitions.scalars[key])
        }))
    }

    private async generateScalar(scalar: ScalarQuantity): Promise<void> {
        if (!this.fixScalarData(scalar)) {
            return
        }

        let text: string = this.templateReader.scalarTemplate

        const interfacesText: string = this.composeInterfacesText(scalar)
        text = text.replace(new RegExp('#Interfaces#', 'g'), interfacesText)
        text = text.replace(new RegExp('#CommaIfInterface#', 'g'), interfacesText.length > 0 ? ',' : '')

        const basesText: string = this.composeBasesText(scalar)
        text = text.replace(new RegExp('#Bases#', 'g'), basesText)

        const fromText: string = this.composeFromText(scalar)
        text = text.replace(new RegExp('#From#', 'g'), fromText)

        const unitsText: string = this.composeUnitsText(scalar)
        text = text.replace(new RegExp('#Units#', 'g'), unitsText)

        const powersText: string = this.composePowersText(scalar)
        text = text.replace(new RegExp('#Powers#', 'g'), powersText)

        const invertDoubleText: string = this.composeInversionOperatorDoubleText(scalar)
        text = text.replace(new RegExp('#InversionOperatorDouble#', 'g'), invertDoubleText)

        const invertScalarText: string = this.composeInversionOperatorScalarText(scalar)
        text = text.replace(new RegExp('#InversionOperatorScalar#', 'g'), invertScalarText)

        const magnitudeFromUnitDoubleText: string = this.composeMagnitudeFromUnitDoubleText(scalar)
        text = text.replace(new RegExp('#MagnitudeFromUnitDouble#', 'g'), magnitudeFromUnitDoubleText)

        const magnitudeFromUnitScalarText: string = this.composeMagnitudeFromUnitScalarText(scalar)
        text = text.replace(new RegExp('#MagnitudeFromUnitScalar#', 'g'), magnitudeFromUnitScalarText)

        const quantityToUnitText: string = this.composeQuantityToUnitText(scalar)
        text = text.replace(new RegExp('#QuantityToUnit#', 'g'), quantityToUnitText)

        const convertibleText: string = this.composeConvertibleText(scalar)
        text = text.replace(new RegExp('#Convertible#', 'g'), convertibleText)

        const vectorText: string = this.composeToVectorText(scalar)
        text = text.replace(new RegExp('#ToVector#', 'g'), vectorText)

        text = this.insertNames(text, scalar)

        text = text.replace(new RegExp('\t', 'g'), '    ')

        text = await Documenter.document(text, this.documentationDirectory + '\\Scalars\\' + scalar.name + '.txt')

        text = insertAppropriateNewlines(text)
        text = normalizeLineEndings(text)
        text = removeConsecutiveNewlines(text)
        text = normalizeLineEndings(text)

        await fsp.writeFile(this.destination + '\\' + scalar.name + '.g.cs', text)
    }

    private fixScalarData(scalar: ScalarQuantity): boolean {
        const requiredEntries: string[] = ['name', 'type', 'baseUnits', 'unit', 'unitBias',
            'vector', 'inverse', 'square', 'cube', 'squareRoot', 'cubeRoot', 'units', 'convertible']
        const optionalEntries: string[] = []

        const missingEntries: string[] = []
        for (let requiredEntry of requiredEntries) {
            if (!(requiredEntry in scalar)) {
                missingEntries.push(requiredEntry)
            }
        }

        if (missingEntries.length > 0) {
            this.reportErrorMissingEntries(scalar, missingEntries)
            return false
        }

        const redudantEntries: string[] = []
        for (let entry of Object.keys(scalar)) {
            if (!(entry in requiredEntries || entry in optionalEntries))
            {
                requiredEntries.push(entry)
            }
        }

        if (redudantEntries.length > 0) {
            this.reportWarningRedundantEntries(scalar, redudantEntries)
        }

        scalar.baseUnits = getBaseUnits(this.definitionReader.definitions, scalar)
        scalar.unit = getUnitName(this.definitionReader.definitions, scalar)
        scalar.unitBias = getUnitBias(this.definitionReader.definitions, scalar)
        scalar.vector = getNameOfVectorVersionOfScalar(this.definitionReader.definitions, scalar)
        scalar.inverse = getInverse(this.definitionReader.definitions, scalar)
        scalar.square = getSquare(this.definitionReader.definitions, scalar)
        scalar.cube = getCube(this.definitionReader.definitions, scalar)
        scalar.squareRoot = getSquareRoot(this.definitionReader.definitions, scalar)
        scalar.cubeRoot = getCubeRoot(this.definitionReader.definitions, scalar)
        scalar.units = getUnits(this.definitionReader.definitions, scalar)
        scalar.symbol = getSymbol(this.definitionReader.definitions, scalar)
        scalar.bases = getBases(this.definitionReader.definitions, scalar)
        scalar.convertible = getConvertible(this.definitionReader.definitions, scalar)

        if (scalar.vector) {
            scalar.vectorDimensionalities = getDimensionalitiesOfVector(this.definitionReader.definitions, this.definitionReader.definitions.vectors[scalar.vector])
        } else {
            scalar.vectorDimensionalities = []
        }

        return true
    }

    private insertNames(text: string, scalar: ScalarQuantity): string {
        text = text.replace(new RegExp('#Unit#', 'g'), scalar.unit)
        text = text.replace(new RegExp('#UnitVariable#', 'g'), lowerCase(scalar.unit))

        const unitListTexts = createUnitListTexts(scalar)
        text = text.replace(new RegExp('#SingularUnits#', 'g'), unitListTexts.singular)
        text = text.replace(new RegExp('#PluralUnits#', 'g'), unitListTexts.plural)

        const powers: { name: string, data: string | string[] | false }[] = [
            { name: 'Inverse', data: scalar.inverse },
            { name: 'Square', data: scalar.square },
            { name: 'Cube', data: scalar.cube },
            { name: 'SquareRoot', data: scalar.squareRoot },
            { name: 'CubeRoot', data: scalar.cubeRoot }
        ]

        for (let power of powers) {
            if (power.data && power.data.length > 0) {
                text = text.replace(new RegExp('#' + power.name + 'Quantity#', 'g'), power.data[0])
                text = text.replace(new RegExp('#' + power.name + 'QuantityVariable#', 'g'), lowerCase(power.data[0]))
    
                for (let i = 0; i < power.data.length; i++) {
                    const quantity: string = power.data[i];
                    text = text.replace(new RegExp('#' + power.name + 'Quantity' + i + '#', 'g'), quantity)
                    text = text.replace(new RegExp('#' + power.name + 'Quantity' + i + 'Variable#', 'g'), lowerCase(quantity))
                }
            }
        }

        if (scalar.vector) {
            text = text.replace(new RegExp('#VectorQuantity#', 'g'), scalar.vector)
        }

        if (scalar.symbol) {
            text = text.replace(new RegExp('#Abbreviation#', 'g'), scalar.symbol)
        }

        text = text.replace(new RegExp('#Quantity#', 'g'), scalar.name)
        text = text.replace(new RegExp('#quantity#', 'g'), lowerCase(scalar.name))
        return text
    }

    private composeInterfacesText(scalar: ScalarQuantity): string {
        const powers: { name: string, data: string | string[] | false, expression: (quantityName: string) => string }[] = [
            { name: 'Inverse', data: scalar.inverse, expression: (x) => 'IInvertibleScalarQuantity<' + x + '>' },
            { name: 'Square', data: scalar.square, expression: (x) => 'ISquarableScalarQuantity<' + x + '>' },
            { name: 'Cube', data: scalar.cube, expression: (x) => 'ICubableScalarQuantity<' + x + '>' },
            { name: 'SquareRoot', data: scalar.squareRoot, expression: (x) => 'ISquareRootableScalarQuantity<' + x + '>' },
            { name: 'CubeRoot', data: scalar.cubeRoot, expression: (x) => 'ICubeRootableScalarQuantity<' + x + '>' }
        ]

        const interfaces: string[] = []

        for (let power of powers) {
            if (power.data && power.data.length > 0) {
                interfaces.push(power.expression('#' + power.name + 'Quantity#'))
            }
        }

        interfaces.push('IMultiplicableScalarQuantity<#Quantity#, Scalar>')
        interfaces.push('IMultiplicableScalarQuantity<Unhandled, Unhandled>')

        interfaces.push('IDivisibleScalarQuantity<#Quantity#, Scalar>')
        interfaces.push('IDivisibleScalarQuantity<Unhandled, Unhandled>')

        interfaces.push('IGenericallyMultiplicableScalarQuantity')
        interfaces.push('IGenericallyDivisibleScalarQuantity')

        if (scalar.vectorDimensionalities) {
            for (let dimensionality of scalar.vectorDimensionalities) {
                interfaces.push('IVector' + dimensionality + 'MultiplicableScalarQuantity<#VectorQuantity#' + dimensionality + ', Vector' + dimensionality + '>')
            }
        }

        let interfacesText: string = ''
        for (let _interface of interfaces) {
            interfacesText += '\t' + _interface + ',\n'
        }

        return interfacesText.slice(0, -2)
    }

    private composeBasesText(scalar: ScalarQuantity): string {
        let basesText: string = ''

        if (Array.isArray(scalar.bases)) {
            for (let base of scalar.bases) {
                if (base.special) {
                    if (base.separator) {
                        basesText += '\n'
                    }
                } else {
                    basesText += '\t#Document:OneUnit(#Quantity#, #Unit#, ' + base.singular + ')#\n'
                    basesText += '\tpublic static #Quantity# One' + base.singular + ' { get; } = new(1, #Unit#.' + base.singular + ');\n'
                }
            }
        }

        return basesText
    }

    private composeFromText(scalar: ScalarQuantity): string {
        const powers: { name: string, data: string | string[] | false, expression: (variableName: string) => string }[] = [
            { name: 'Inverse', data: scalar.inverse, expression: (x) => '1 / ' + x + '.Magnitude' },
            { name: 'Square', data: scalar.square, expression: (x) => 'Math.Sqrt(' + x + '.Magnitude)' },
            { name: 'Cube', data: scalar.cube, expression: (x) => 'Math.Cbrt(' + x + '.Magnitude)' },
            { name: 'SquareRoot', data: scalar.squareRoot, expression: (x) => 'Math.Pow(' + x + '.Magnitude, 2)' },
            { name: 'CubeRoot', data: scalar.cubeRoot, expression: (x) => 'Math.Pow(' + x + '.Magnitude, 3)' }
        ]

        let fromText: string = ''
        
        for (let power of powers) {
            if (Array.isArray(power.data)) {
                for (let i = 0; i < power.data.length; i++) {
                    const quantity: string = power.name + 'Quantity' + i
                    const variable: string = quantity + 'Variable'
                    fromText += '\t#Document:From' + power.name + '(#Quantity#, #' + quantity + '#, #' + variable + '#)#\n'
                    fromText += '\tpublic static #Quantity# From(#' + quantity + '# #' + variable + '#) => new(' + power.expression('#' + variable + '#') + ');\n'
                }
            }
        }

        return fromText
    }

    private composeUnitsText(scalar: ScalarQuantity): string {
        let unitsText: string = ''

        if (Array.isArray(scalar.units)) {
            for (let unit of scalar.units) {
                if (unit.special) {
                    if (unit.separator) {
                        unitsText += '\n'
                    }
                } else {
                    unitsText += '\t#Document:InUnit(#Quantity#, #Unit#, ' + unit.singular + ')#\n'
                    unitsText += '\tpublic Scalar In' + unit.plural + ' => InUnit(#Unit#.' + unit.singular + ');\n'
                }
            }
        }

        return unitsText
    }

    private composePowersText(scalar: ScalarQuantity): string {
        const powers: { powerName: string, data: string | string[] | false, methodName: string }[] = [
            { powerName: 'Inverse', data: scalar.inverse, methodName: 'Invert' },
            { powerName: 'Square', data: scalar.square, methodName: 'Square' },
            { powerName: 'Cube', data: scalar.cube, methodName: 'Cube' },
            { powerName: 'SquareRoot', data: scalar.squareRoot, methodName: 'SquareRoot' },
            { powerName: 'CubeRoot', data: scalar.cubeRoot, methodName: 'CubeRoot' }
        ]
        
        let powersText: string = ''

        for (let power of powers) {
            if (power.data && power.data.length > 0) {
                const quantity: string = power.powerName + 'Quantity'
                powersText += '\t#Document:' + power.methodName + '(#Quantity#, #' + quantity + '#)#\n'
                powersText += '\tpublic #' + quantity + '# ' + power.methodName + '() => #' + quantity + '#.From(this);\n'
            }
        }

        return powersText
    }

    private composeInversionOperatorDoubleText(scalar: ScalarQuantity): string {
        let invertText: string = ''

        if (scalar.inverse && scalar.inverse.length > 0) {
            invertText += '\#Document:DivideDoubleOperatorRHS(#Quantity#, #InverseQuantity#)#\n'
            invertText += '\tpublic static #InverseQuantity# operator /(double x, #Quantity# y) => x * y.Invert();\n'
        }

        return invertText
    }

    private composeInversionOperatorScalarText(scalar: ScalarQuantity): string {
        let invertText: string = ''

        if (scalar.inverse && scalar.inverse.length > 0) {
            invertText += '\#Document:DivideScalarOperatorRHS(#Quantity#, #InverseQuantity#)#\n'
            invertText += '\tpublic static #InverseQuantity# operator /(Scalar x, #Quantity# y) => x * y.Invert();\n'
        }

        return invertText
    }

    private composeMagnitudeFromUnitDoubleText(scalar: ScalarQuantity): string {
        if (scalar.unitBias === true) {
            return '(magnitude * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale'
        } else {
            return 'magnitude * #UnitVariable#.Factor'
        }
    }

    private composeMagnitudeFromUnitScalarText(scalar: ScalarQuantity): string {
        if (scalar.unitBias === true) {
            return '(magnitude.Magnitude * #UnitVariable#.Prefix.Scale + #UnitVariable#.Bias) * #UnitVariable#.BaseScale'
        } else {
            return 'magnitude.Magnitude * #UnitVariable#.Factor'
        }
    }

    private composeQuantityToUnitText(scalar: ScalarQuantity): string {
        if (scalar.unitBias === true) {
            return '(#quantity#.Magnitude / #UnitVariable#.BaseScale - #UnitVariable#.Bias) / #UnitVariable#.Prefix.Scale'
        } else {
            return '#quantity#.Magnitude / #UnitVariable#.Factor'
        }
    }

    private composeConvertibleText(scalar: ScalarQuantity): string {
        let convertibleText: string = ''

        if (Array.isArray(scalar.convertible)) {
            for (let convertible of scalar.convertible) {
                convertibleText += '\t#Document:AsShared(quantity = #Quantity#, sharedQuantity = ' + convertible + ')#\n'
                convertibleText += '\tpublic ' + convertible + ' As' + convertible + ' => new(Magnitude);\n'
            }
        }

        return convertibleText
    }

    private composeToVectorText(scalar: ScalarQuantity): string {
        let toVectorMethods: string = ''
        let toVectorOperations: string = ''

        if (Array.isArray(scalar.vectorDimensionalities)) {
            for (let dimensionality of scalar.vectorDimensionalities) {
                const argument: string = '(quantity = #Quantity#, vectorQuantity = #VectorQuantity#, n = #Dimensionality#)'

                let doubleTupleDefinition: string = ''
                let scalarTupleDefinition: string = ''
                for (let componentName of getVectorComponentNames(dimensionality)) {
                    doubleTupleDefinition += 'double ' + lowerCase(componentName) + ', '
                    scalarTupleDefinition += 'Scalar ' + lowerCase(componentName) + ', '
                }
                doubleTupleDefinition = '(' + doubleTupleDefinition.slice(0, -2) + ')'
                scalarTupleDefinition = '(' + scalarTupleDefinition.slice(0, -2) + ')'

                toVectorMethods += '\t#Document:MultiplyVectorNMethod' + argument + '#\n'
                toVectorMethods += '\tpublic #VectorQuantity##Dimensionality# Multiply(Vector#Dimensionality# vector) #newline#=> new(vector * Magnitude);\n'

                toVectorMethods += '\t#Document:MultiplyTupleNMethod' + argument + '#\n'
                toVectorMethods += '\tpublic #VectorQuantity##Dimensionality# Multiply(#newline#' + doubleTupleDefinition + ' components) #newline#'
                toVectorMethods += '=> Multiply(new Vector#Dimensionality#(components));\n'

                toVectorMethods += '\t#Document:MultiplyScalarTupleNMethod' + argument + '#\n'
                toVectorMethods += '\tpublic #VectorQuantity##Dimensionality# Multiply(#newline#' + scalarTupleDefinition + ' components) #newline#'
                toVectorMethods += '=> Multiply(new Vector#Dimensionality#(components));\n'

                toVectorOperations += '\t#Document:MultiplyVectorNOperatorLHS' + argument + '#\n'
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(#Quantity# a, Vector#Dimensionality# b) #newline#=> a.Multiply(b);\n'

                toVectorOperations += '\t#Document:MultiplyVectorNOperatorRHS' + argument + '#\n'
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(Vector#Dimensionality# a, #Quantity# b) #newline#=> b.Multiply(a);\n'

                toVectorOperations += '\t#Document:MultiplyTupleNOperatorLHS' + argument + '#\n'
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(#Quantity# a, ' + doubleTupleDefinition + ' b) #newline#=> a.Multiply(b);\n'

                toVectorOperations += '\t#Document:MultiplyTupleNOperatorRHS' + argument + '#\n'
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(' + doubleTupleDefinition + ' a, #Quantity# b) #newline#=> b.Multiply(a);\n'

                toVectorOperations += '\t#Document:MultiplyScalarTupleNOperatorLHS' + argument + '#\n'
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(#Quantity# a, ' + scalarTupleDefinition + ' b) #newline#=> a.Multiply(b);\n'

                toVectorOperations += '\t#Document:MultiplyScalarTupleNOperatorRHS' + argument + '#\n'
                toVectorOperations += '\tpublic static #VectorQuantity##Dimensionality# operator *(' + scalarTupleDefinition + ' a, #Quantity# b) #newline#=> b.Multiply(a);\n'

                toVectorMethods = toVectorMethods.replace(new RegExp('#Dimensionality#', 'g'), dimensionality.toString())
                toVectorOperations = toVectorOperations.replace(new RegExp('#Dimensionality#', 'g'), dimensionality.toString())
            }
        }

        return toVectorMethods + toVectorOperations
    }

    private reportErrorMissingEntries(scalar: ScalarQuantity, entries: string[]): void {
        console.error('Scalar quantity: [' + scalar.name + '] is missing ' + (entries.length > 1 ? 'entries' : 'entry') + ': ' + entries + '.')
    }

    private reportWarningRedundantEntries(scalar: ScalarQuantity, entries: string[]): void {
        console.warn('Scalar quantity: [' + scalar.name + '] has redundant ' + (entries.length > 1 ? 'entries' : 'entry') + ':' + entries + '.')
    }
}