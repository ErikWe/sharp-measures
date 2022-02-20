import fsp from 'fs/promises'
import { Documenter } from './Documenter'
import { DefinitionReader } from './DefinitionReader'
import { TemplateReader } from './TemplateReader'
import { ScalarQuantity } from './ScalarQuantity'
import { VectorQuantity } from './VectorQuantity'
import { Unit } from './Unit'
import { composeUnitsNameList, composeBasesNameList, fixLines, getBases, getConstants, getConvertible, getDefaultConstant, getDefaultUnit, getUnit,
    getUnitQuantity, getUnits, getVectorComponentNames, getVectorVersionOfScalar, lowerCase, parseUnitPlural, getUnitUnbiasedQuantity } from './Utility'

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
        let text: string = this.templateReader.scalarTemplate

        text = this.setConditionalBlocks(scalar, text)

        const interfacesText: string = this.composeInterfacesText(scalar)
        text = text.replace(/#Interfaces#/g, interfacesText)
        text = text.replace(/#CommaIfInterface#/g, interfacesText.length > 0 ? ',' : '')

        const constantsText: string = this.composeConstantsText(scalar)
        text = text.replace(/#Constants#/g, constantsText)

        const basesText: string = this.composeBasesText(scalar)
        text = text.replace(/#Bases#/g, basesText)

        const fromText: string = this.composeFromText(scalar)
        text = text.replace(/#From#/g, fromText)

        const unitsText: string = this.composeUnitsText(scalar)
        text = text.replace(/#Units#/g, unitsText)

        const constantMultiplesText: string = this.composeConstantMultiplesText(scalar)
        text = text.replace(/#ConstantMultiples#/g, constantMultiplesText)

        const powersText: string = this.composePowersText(scalar)
        text = text.replace(/#Powers#/g, powersText)

        const invertDoubleText: string = this.composeInversionOperatorDoubleText(scalar)
        text = text.replace(/#InversionOperatorDouble#/g, invertDoubleText)

        const invertScalarText: string = this.composeInversionOperatorScalarText(scalar)
        text = text.replace(/#InversionOperatorScalar#/g, invertScalarText)

        const magnitudeFromUnitDoubleText: string = this.composeMagnitudeFromUnitDoubleText(scalar)
        text = text.replace(/#MagnitudeFromUnitDouble#/g, magnitudeFromUnitDoubleText)

        const quantityToUnitText: string = this.composeQuantityToUnitText(scalar)
        text = text.replace(/#QuantityToUnit#/g, quantityToUnitText)

        const convertibleText: string = this.composeConvertibleText(scalar)
        text = text.replace(/#Convertible#/g, convertibleText)

        const vectorText: string = this.composeToVectorText(scalar)
        text = text.replace(/#ToVector#/g, vectorText)

        text = this.insertNames(text, scalar)

        if (await fsp.stat(this.documentationDirectory + '\\Scalars\\' + scalar.name + '.txt').catch(() => false)) {
            text = await Documenter.document(text, this.documentationDirectory + '\\Scalars\\' + scalar.name + '.txt')
        } else {
            this.reportErrorDocumentationFileNotFound(scalar, this.documentationDirectory + '\\Scalars\\' + scalar.name + '.txt')
        }

        text = fixLines(text)

        await fsp.writeFile(this.destination + '\\' + scalar.name + '.g.cs', text)
    }

    private insertNames(text: string, scalar: ScalarQuantity): string {
        const unit: Unit = getUnit(this.definitionReader.definitions, scalar)
        text = text.replace(/#Unit#/g, 'UnitOf' + unit.name)
        text = text.replace(/#UnitVariable#/g, lowerCase('UnitOf' + unit.name))

        text = text.replace(/#UnitQuantity#/g, getUnitQuantity(this.definitionReader.definitions, unit).name)

        text = text.replace(/#UnbiasedQuantity#/g, unit.unbiasedQuantity ? unit.unbiasedQuantity : 'NoUnbiasedQuantityError')

        text = this.insertDefaultUnitNames(text, scalar)
        text = this.insertPowerNames(text, scalar)

        text = text.replace(/#VectorQuantity#/g, getVectorVersionOfScalar(this.definitionReader.definitions, scalar)?.name ?? 'NoVectorVersionError')

        text = text.replace(/#SingularUnits#/g, composeUnitsNameList(this.definitionReader.definitions, scalar).singular)
        text = text.replace(/#PluralUnits#/g, composeUnitsNameList(this.definitionReader.definitions, scalar).plural)
        text = text.replace(/#SingularBases#/g, composeBasesNameList(this.definitionReader.definitions, scalar).singular)
        text = text.replace(/#PluralBases#/g, composeBasesNameList(this.definitionReader.definitions, scalar).plural)

        text = text.replace(/#Quantity#/g, scalar.name)
        text = text.replace(/#quantity#/g, lowerCase(scalar.name))
        return text
    }

    private insertDefaultUnitNames(text: string, scalar: ScalarQuantity): string {
        let defaultUnit: Unit['units'][number] | NonNullable<Unit['constants']>[number] | undefined = getDefaultUnit(this.definitionReader.definitions, scalar)
        let isConstant: boolean = false

        if (defaultUnit === undefined) {
            defaultUnit = getDefaultConstant(this.definitionReader.definitions, scalar)
            isConstant = true
        }

        if (defaultUnit !== undefined && !defaultUnit.special) {
            text = text.replace(/#DefaultUnit#/g, defaultUnit.name)
            text = text.replace(/#DefaultUnits#/g, parseUnitPlural(defaultUnit.name, defaultUnit.plural) + (isConstant ? 'Multiples' : ''))
            if (defaultUnit.symbol === undefined) {
                this.reportErrorMissingDefaultUnitSymbol(scalar)
                text = text.replace(/#DefaultSymbol#/g, 'NoDefaultUnitSymbolError')
            } else {
                text = text.replace(/#DefaultSymbol#/g, defaultUnit.symbol)
            }
        } else {
            this.reportErrorMissingDefaultUnit(scalar)
            text = text.replace(/#DefaultUnit#/g, 'NoDefaultUnitError')
            text = text.replace(/#DefaultUnits#/g, 'NoDefaultUnitError')
            text = text.replace(/#DefaultSymbol#/g, 'NoDefaultUnitError')
        }

        return text
    }

    private insertPowerNames(text: string, scalar: ScalarQuantity): string {
        const powers: { name: string, data: string[] | undefined }[] = [
            { name: 'Inverse', data: scalar.inverse },
            { name: 'Square', data: scalar.square },
            { name: 'Cube', data: scalar.cube },
            { name: 'SquareRoot', data: scalar.squareRoot },
            { name: 'CubeRoot', data: scalar.cubeRoot }
        ]

        for (let power of powers) {
            if (power.data !== undefined && power.data.length > 0) {
                text = text.replace(new RegExp('#' + power.name + 'Quantity#', 'g'), power.data[0])
                text = text.replace(new RegExp('#' + power.name + 'QuantityVariable#', 'g'), lowerCase(power.data[0]))
    
                for (let i = 0; i < power.data.length; i++) {
                    const quantity: string = power.data[i];
                    text = text.replace(new RegExp('#' + power.name + 'Quantity' + i + '#', 'g'), quantity)
                    text = text.replace(new RegExp('#' + power.name + 'Quantity' + i + 'Variable#', 'g'), lowerCase(quantity))
                }
            }
        }

        return text
    }

    private setConditionalBlocks(scalar: ScalarQuantity, text: string): string {
        if (scalar.unitBias) {
            text = text.replace(/(\n|\r\n|\r?)#Unbiased#([^]+?)#\/Unbiased#/g, '')
        } else {
            text = text.replace(/(\n|\r\n|\r?)#(\/?)Unbiased#/g, '')
        }
        
        return text
    }

    private composeInterfacesText(scalar: ScalarQuantity): string {
        const powers: { name: string, data: string[] | undefined, expression: (quantityName: string) => string }[] = [
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

        const vector: VectorQuantity | undefined = getVectorVersionOfScalar(this.definitionReader.definitions, scalar)
        if (vector) {
            for (let dimensionality of vector.dimensionalities) {
                interfaces.push('IVector' + dimensionality + 'MultiplicableScalarQuantity<#VectorQuantity#' + dimensionality + ', Vector' + dimensionality + '>')
            }
        }

        let interfacesText: string = ''
        for (let _interface of interfaces) {
            interfacesText += '\t' + _interface + ',\n'
        }

        return interfacesText.slice(0, -2)
    }

    private composeConstantsText(scalar: ScalarQuantity): string {
        const unit: Unit = getUnit(this.definitionReader.definitions, scalar)
        const unitQuantity: ScalarQuantity | undefined = unit.bias ?
            getUnitUnbiasedQuantity(this.definitionReader.definitions, unit) :
            getUnitQuantity(this.definitionReader.definitions, unit)
            const constants: Unit['constants'] = getConstants(this.definitionReader.definitions, scalar)

        if (unitQuantity === undefined || constants === undefined) {
            return ''
        }

        let constantsText: string = ''

        for (let constant of constants) {
            if (constant.special) {
                if (constant.separator) {
                    constantsText += '\n'
                }
            } else {
                constantsText += '\t#Document:Constant(#Quantity#, #Unit#, ' + constant.name + ')#\n'
                constantsText += '\tpublic static #Quantity# ' + constant.name + ' { get; } = '
                
                if (unitQuantity === scalar) {
                    constantsText += '#Unit#.' + constant.name + '.' + unitQuantity.name + ';\n'
                } else if ((getConvertible(this.definitionReader.definitions, unitQuantity) as Array<ScalarQuantity | VectorQuantity>).includes(scalar)) {
                    constantsText += '#Unit#.' + constant.name + '.' + unitQuantity.name + '.As#Quantity#;\n'
                } else {
                    constantsText += 'new(1, #Unit#.' + constant.name + ');\n'
                }
            }
        }

        return constantsText
    }

    private composeBasesText(scalar: ScalarQuantity): string {
        const unit: Unit = getUnit(this.definitionReader.definitions, scalar)
        const unitQuantity: ScalarQuantity | undefined = unit.bias ?
            getUnitUnbiasedQuantity(this.definitionReader.definitions, unit) :
            getUnitQuantity(this.definitionReader.definitions, unit)

        if (unitQuantity === undefined) {
            return ''
        }

        const bases: Unit['units'] = getBases(this.definitionReader.definitions, scalar)

        let basesText: string = ''

        for (let base of bases) {
            if (base.special) {
                if (base.separator) {
                    basesText += '\n'
                }
            } else {
                basesText += '\t#Document:OneUnit(#Quantity#, #Unit#, ' + base.name + ')#\n'
                basesText += '\tpublic static #Quantity# One' + base.name + ' { get; } = '
                
                if (unitQuantity === scalar) {
                    basesText += '#Unit#.' + base.name + '.' + unitQuantity.name + ';\n'
                } else if ((getConvertible(this.definitionReader.definitions, unitQuantity) as Array<ScalarQuantity | VectorQuantity>).includes(scalar)) {
                    basesText += '#Unit#.' + base.name + '.' + unitQuantity.name + '.As#Quantity#;\n'
                } else {
                    basesText += 'new(1, #Unit#.' + base.name + ');\n'
                }
            }
        }

        return basesText
    }

    private composeFromText(scalar: ScalarQuantity): string {
        const powers: { name: string, data: string[] | undefined, expression: (variableName: string) => string }[] = [
            { name: 'Inverse', data: scalar.inverse, expression: (x) => '1 / ' + x + '.Magnitude' },
            { name: 'Square', data: scalar.square, expression: (x) => 'Math.Sqrt(' + x + '.Magnitude)' },
            { name: 'Cube', data: scalar.cube, expression: (x) => 'Math.Cbrt(' + x + '.Magnitude)' },
            { name: 'SquareRoot', data: scalar.squareRoot, expression: (x) => 'Math.Pow(' + x + '.Magnitude, 2)' },
            { name: 'CubeRoot', data: scalar.cubeRoot, expression: (x) => 'Math.Pow(' + x + '.Magnitude, 3)' }
        ]

        let fromText: string = ''
        
        for (let power of powers) {
            if (power.data !== undefined) {
                for (let i = 0; i < power.data.length; i++) {
                    const quantity: string = power.name + 'Quantity' + i
                    const variable: string = quantity + 'Variable'
                    fromText += '\t#Document:From' + power.name + '(#Quantity#, #' + quantity + '#, #' + variable + '#)#\n'
                    fromText += '\tpublic static #Quantity# From(#' + quantity + '# #' + variable + '#) => new(' + power.expression('#' + variable + '#') + ');\n'
                }
            }
        }

        if (scalar.squareRoot !== undefined) {
            for (let i = 0; i < scalar.squareRoot.length; i++) {
                const quantity: string = 'SquareRootQuantity' + i
                const variable: string = quantity + 'Variable'
                fromText += '\t#Document:FromTwoSquareRoot(#Quantity#, #' + quantity + '#, #' + variable + '#)#\n'
                fromText += '\tpublic static #Quantity# From(#' + quantity + '# #' + variable + '#1, #' + quantity + '# #' + variable + '#2) => new(' +
                    '#' + variable + '#1.Magnitude * #' + variable + '#2.Magnitude);\n'
            }
        }

        if (scalar.cubeRoot !== undefined) {
            for (let i = 0; i < scalar.cubeRoot.length; i++) {
                const quantity: string = 'CubeRootQuantity' + i
                const variable: string = quantity + 'Variable'
                fromText += '\t#Document:FromThreeCubeRoot(#Quantity#, #' + quantity + '#, #' + variable + '#)#\n'
                fromText += '\tpublic static #Quantity# From(#' + quantity + '# #' + variable + '#1, #' + quantity + '# #' + variable + '#2, #' + quantity + '# #' +
                    variable +  '#3) => new(' + '#' + variable + '#1.Magnitude * #' + variable + '#2.Magnitude * #' + variable + '#3.Magnitude);\n'
            }
        }

        return fromText
    }

    private composeUnitsText(scalar: ScalarQuantity): string {
        let unitsText: string = ''
        
        const units: Unit['units'] = getUnits(this.definitionReader.definitions, scalar)
        for (let unit of units) {
            if (unit.special) {
                if (unit.separator) {
                    unitsText += '\n'
                }
            } else {
                unitsText += '\t#Document:InUnit(#Quantity#, #Unit#, ' + unit.name + ')#\n'
                unitsText += '\tpublic Scalar ' + parseUnitPlural(unit.name, unit.plural) + ' => InUnit(#Unit#.' + unit.name + ');\n'
            }
        }

        return unitsText
    }

    private composeConstantMultiplesText(scalar: ScalarQuantity): string {
        let unitsText: string = ''

        const constants: Unit['constants'] = getConstants(this.definitionReader.definitions, scalar)
        if (constants !== undefined)
        {
            for (let constant of constants) {
                if (constant.special) {
                    if (constant.separator) {
                        unitsText += '\n'
                    }
                } else {
                    unitsText += '\t#Document:InConstant(#Quantity#, #Unit#, ' + constant.name + ')#\n'
                    unitsText += '\tpublic Scalar ' + parseUnitPlural(constant.name, constant.plural) + 'Multiples => InUnit(#Unit#.' + constant.name + ');\n'
                }
            }
        }

        return unitsText
    }

    private composePowersText(scalar: ScalarQuantity): string {
        const powers: { powerName: string, data: string[] | undefined, methodName: string }[] = [
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
            invertText += '\t#Document:DivideDoubleOperatorRHS(#Quantity#, #InverseQuantity#)#\n'
            invertText += '\tpublic static #InverseQuantity# operator /(double x, #Quantity# y) => new(x / y.Magnitude);\n'
        }

        return invertText.slice(0, -1)
    }

    private composeInversionOperatorScalarText(scalar: ScalarQuantity): string {
        let invertText: string = ''

        if (scalar.inverse && scalar.inverse.length > 0) {
            invertText += '\t#Document:DivideScalarOperatorRHS(#Quantity#, #InverseQuantity#)#\n'
            invertText += '\tpublic static #InverseQuantity# operator /(Scalar x, #Quantity# y) => new(x / y.Magnitude);\n'
        }

        return invertText.slice(0, -1)
    }

    private composeMagnitudeFromUnitDoubleText(scalar: ScalarQuantity): string {
        if (scalar.unitBias === true) {
            return '(magnitude * #UnitVariable#.#UnbiasedQuantity#.Magnitude) - #UnitVariable#.Offset'
        } else if (getUnit(this.definitionReader.definitions, scalar).bias) {
            return 'magnitude * #UnitVariable#.#UnbiasedQuantity#.Magnitude'
        } else {
            return 'magnitude * #UnitVariable#.#UnitQuantity#.Magnitude'
        }
    }

    private composeQuantityToUnitText(scalar: ScalarQuantity): string {
        if (scalar.unitBias === true) {
            return '(#quantity#.Magnitude + #UnitVariable#.Offset) / #UnitVariable#.#UnbiasedQuantity#.Magnitude'
        } else if (getUnit(this.definitionReader.definitions, scalar).bias) {
            return '#quantity#.Magnitude / #UnitVariable#.#UnbiasedQuantity#.Magnitude'
        } else {
            return '#quantity#.Magnitude / #UnitVariable#.#UnitQuantity#.Magnitude'
        }
    }

    private composeConvertibleText(scalar: ScalarQuantity): string {
        let convertibleText: string = ''

        for (let convertible of getConvertible(this.definitionReader.definitions, scalar)) {
            convertibleText += '\t#Document:AsShared(quantity = #Quantity#, sharedQuantity = ' + convertible.name + ')#\n'
            convertibleText += '\tpublic ' + convertible.name + ' As' + convertible.name + ' => new(Magnitude);\n'
        }

        return convertibleText.slice(0, -1)
    }

    private composeToVectorText(scalar: ScalarQuantity): string {
        let toVectorMethods: string = ''
        let toVectorOperations: string = ''

        const vector: VectorQuantity | undefined = getVectorVersionOfScalar(this.definitionReader.definitions, scalar)
        if (vector) {
            for (let dimensionality of vector.dimensionalities) {
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
                toVectorMethods += '\tpublic #VectorQuantity##Dimensionality# Multiply(Vector#Dimensionality# factor) #newline#=> new(factor * Magnitude);\n'

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

                toVectorMethods = toVectorMethods.replace(/#Dimensionality#/g, dimensionality.toString())
                toVectorOperations = toVectorOperations.replace(/#Dimensionality#/g, dimensionality.toString())
            }
        }

        return toVectorMethods + toVectorOperations
    }

    private reportErrorDocumentationFileNotFound(scalar: ScalarQuantity, fileName: string): void {
        console.error('Could not locate documentation file for scalar quantity: [' + scalar.name + '], tried: [' + fileName + '].')
    }

    private reportErrorMissingDefaultUnitSymbol(scalar: ScalarQuantity): void {
        const defaultUnit: Unit['units'][number] | NonNullable<Unit['constants']>[number] | undefined = getDefaultUnit(this.definitionReader.definitions, scalar)
        if (defaultUnit === undefined) {
            getDefaultConstant(this.definitionReader.definitions, scalar)
        }

        if (defaultUnit === undefined || defaultUnit.special) {
            console.error('Default unit of scalar quantity: [' + scalar.name + '] is missing symbol.')
        } else {
            console.error('Default unit: [' + defaultUnit.name + '] of scalar quantity: [' + scalar.name + '] is missing symbol.')
        }
    }

    private reportErrorMissingDefaultUnit(scalar: ScalarQuantity): void {
        console.error('Could not identify default unit of scalar quantity: [' + scalar.name + '].')
    }
}