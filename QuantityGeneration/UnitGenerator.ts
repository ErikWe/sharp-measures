import fsp from 'fs/promises'
import { Documenter } from './Documenter'
import { DefinitionReader } from './DefinitionReader'
import { Unit } from './Unit'
import { TemplateReader } from './TemplateReader'
import { getUnitQuantity, insertAppropriateNewlines, normalizeLineEndings, removeConsecutiveNewlines, parseUnitPlural, lowerCase } from './Utility'
import { ScalarQuantity } from './ScalarQuantity'

export class UnitGenerator {
    public constructor(private destination: string, private documentationDirectory: string,
        private definitionReader: DefinitionReader, private templateReader: TemplateReader) {}

    public async generate(): Promise<void> {
        const keys: string[] = Object.keys(this.definitionReader.definitions.units)

        await Promise.all(keys.map(async (key: string) => {
            this.generateUnits(this.definitionReader.definitions.units[key])
        }))
    }

    private async generateUnits(unit: Unit): Promise<void> {
        let text: string = unit.bias ? this.templateReader.biasedUnitTemplate : this.templateReader.unitTemplate

        const derivedText: string = this.composeDerivedText(unit)
        text = text.replace(/#Derived#/g, derivedText)
        text = text.replace(/#NewlineIfDerived#/g, derivedText === '' ? '' : '\n')

        const unitsText: string = this.composeUnitsText(unit)
        text = text.replace(/#Units#/g, unitsText)

        text = this.insertNames(text, unit)

        text = text.replace(/\t/g, '    ')

        if (await fsp.stat(this.documentationDirectory + '\\Units\\' + unit.name + '.txt').catch(() => false)) {
            text = await Documenter.document(text, this.documentationDirectory + '\\Units\\' + unit.name + '.txt')
        } else {
            this.reportErrorDocumentationFileNotFound(unit, this.documentationDirectory + '\\Units\\' + unit.name + '.txt')
        }

        text = insertAppropriateNewlines(text, 175)
        text = normalizeLineEndings(text)
        text = removeConsecutiveNewlines(text)
        text = normalizeLineEndings(text)

        await fsp.writeFile(this.destination + '\\UnitOf' + unit.name + '.g.cs', text)
    }

    private insertNames(text: string, unit: Unit): string {
        text = text.replace(/#Unit#/g, 'UnitOf' + unit.name)

        const quantity: ScalarQuantity = getUnitQuantity(this.definitionReader.definitions, unit)
        text = text.replace(/#Quantity#/g, quantity.name)
        text = text.replace(/#quantity#/g, lowerCase(quantity.name))
        
        const siUnit: { name: string, symbol: string, plural: string } = this.getSIUnit(unit)
        text = text.replace(/#SIUnit#/g, siUnit.name)
        text = text.replace(/#SIUnits#/g, siUnit.plural)
        text = text.replace(/#SIUnitSymbolic#/g, siUnit.symbol)

        text = text.replace(/#UnbiasedQuantity#/g, unit.unbiasedQuantity ? unit.unbiasedQuantity : 'NoUnbiasedQuantityError')
        text = text.replace(/#unbiasedQuantity#/g, unit.unbiasedQuantity ? lowerCase(unit.unbiasedQuantity) : 'NoUnbiasedQuantityError')

        return text
    }

    private composeUnitsText(unit: Unit): string {
        let unitsText: string = ''

        for (let unitDefinition of unit.units) {
            if (unitDefinition.special === true) {
                if (unitDefinition.separator === true) {
                    unitsText += '\n'
                }
            } else {
                unitsText += '\t#Document:Unit' + unitDefinition.name + '#\n'
                unitsText += '\tpublic static #Unit# ' + unitDefinition.name + ' { get; } = ' + this.parseUnitExpression(unit, unitDefinition) + ';\n'
            }
        }

        return unitsText
    }

    private composeDerivedText(unit: Unit): string {
        if (unit.derived === undefined) {
            return ''
        }
        
        let derivedText: string = ''

        for (let derived of unit.derived) {
            derivedText += '\t#Document:From'

            for (let signatureComponent of derived.signature) {
                derivedText += signatureComponent
            }

            derivedText += '(#Unit#, #SIUnit#)#\n'

            derivedText += '\tpublic static #Unit# From('

            const signatureVariables = this.nameSignatureVariables(derived.signature)
            
            for (let i = 0; i < derived.signature.length; i++) {
                derivedText += 'UnitOf' + derived.signature[i] + ' ' + signatureVariables[i] + ', '
            }

            derivedText = derivedText.slice(0, -2) + ') #newline#=> new(' + this.parseExpression(derived.expression, derived.signature, signatureVariables) + ');\n'
        }

        return derivedText
    }

    private parseUnitExpression(unit: Unit, unitDefinition : Unit['units'][number]): string | undefined {
        if (unitDefinition.special === true) {
            return undefined
        }

        if (unitDefinition.value !== undefined) {
            return this.parseValueUnitExpression(unit, unitDefinition)
        } else if (unitDefinition.alias !== undefined) {
            return this.parseAliasUnitExpression(unit, unitDefinition)
        } else if (unitDefinition.derived !== undefined) {
            return this.parseDerivedUnitExpression(unit, unitDefinition)
        } else if (unitDefinition.scaled !== undefined) {
            return this.parseScaledUnitExpression(unit, unitDefinition)
        } else if (unitDefinition.prefix !== undefined) {
            return this.parsePrefixUnitExpression(unit, unitDefinition)
        } else if (unitDefinition.offset !== undefined) {
            return this.parseOffsetUnitExpression(unit, unitDefinition)
        } else {
            this.reportErrorUnitExpressionParsing(unit, unitDefinition)
            return 'UnitExpressionParsingError'
        }
    }

    private parseValueUnitExpression(unit: Unit, unitDefinition : Unit['units'][number]): string | undefined {
        if (unitDefinition.special === true) {
            return undefined
        }

        if (unit.bias) {
            return 'new(new ' + unit.unbiasedQuantity + '(' + unitDefinition.value + '), new Scalar(' + unitDefinition.bias + '))'
        } else {
            return 'new(new #Quantity#(' + unitDefinition.value + '))'
        }
    }

    private parseAliasUnitExpression(unit: Unit, unitDefinition: Unit['units'][number]): string | undefined {
        if (unitDefinition.special === true) {
            return undefined
        }

        if (unit.bias) {
            return 'new(' + unitDefinition.alias + '.#UnbiasedQuantity#, new Scalar(' + unitDefinition.bias + '))'
        } else {
            return unitDefinition.alias
        }
    }

    private parseDerivedUnitExpression(unit: Unit, unitDefinition: Unit['units'][number]): string | undefined {
        if (unitDefinition.special === true || unitDefinition.derived === undefined) {
            return undefined
        }

        let derivedText: string = 'From('

            for (let derivationElement of unitDefinition.derived) {
                derivedText += 'UnitOf' + derivationElement + ', '
            }

            return derivedText.slice(0, -2) + ')'
    }

    private parseScaledUnitExpression(unit: Unit, unitDefinition: Unit['units'][number]): string | undefined {
        if (unitDefinition.special === true || unitDefinition.scaled === undefined) {
            return undefined
        }

        return unitDefinition.scaled.from + '.ScaledBy(' + unitDefinition.scaled.by + ')'
    }

    private parsePrefixUnitExpression(unit: Unit, unitDefinition: Unit['units'][number]): string | undefined {
        if (unitDefinition.special === true || unitDefinition.prefix === undefined) {
            return undefined
        }

        return unitDefinition.prefix.from + '.WithPrefix(MetricPrefix.' + unitDefinition.prefix.with + ')'
    }

    private parseOffsetUnitExpression(unit: Unit, unitDefinition: Unit['units'][number]): string | undefined {
        if (unitDefinition.special === true || unitDefinition.offset === undefined) {
            return undefined
        }

        return unitDefinition.offset.from + '.OffsetBy(' + unitDefinition.offset.by + ')'
    }

    private nameSignatureVariables(signature: string[]): string[] {
        const count: Record<string, number> = {}
        const named: Record<string, number> = {}

        for (let signatureComponent of signature) {
            count[signatureComponent] = signatureComponent in count ? count[signatureComponent] + 1 : 1
            named[signatureComponent] = 0
        }

        const nameVariables = (type: string) => 'unitOf' + type + (count[type] > 1 ? ++named[type] : '')
        return signature.map(nameVariables)
    }

    private parseExpression(expression: string, signature: string[], signatureVariables: string[]): string {
        for (let i = 0; i < signatureVariables.length; i++) {
            const unit = this.definitionReader.definitions.units[signature[i]]

            expression = expression.replace(new RegExp('\\{' + (i + 1) + '\\}', 'g'), signatureVariables[i] + '.' +
                (unit.bias ? unit.unbiasedQuantity : getUnitQuantity(this.definitionReader.definitions, unit).name))
        }

        return expression
    }

    private getSIUnit(unit: Unit): { name: string, symbol: string, plural: string } {
        for (let unitDefinition of unit.units) {
            if (!unitDefinition.special && unitDefinition.SI) {
                return { name: unitDefinition.name, symbol: this.getUnitSymbol(unit, unitDefinition), plural: this.getUnitPlural(unit, unitDefinition) }
            }
        }

        this.reportErrorNoSIUnit(unit)
        return { name: 'NoSIUnitError', symbol: 'NoSIUnitError', plural: 'NoSIUnitError' }
    }

    private getUnitSymbol(unit: Unit, unitDefinition: Unit['units'][number]): string {
        if (!unitDefinition.special && unitDefinition.SI && unitDefinition.symbol === undefined) {
            this.reportErrorSIUnitMissingSymbol(unit, unitDefinition)
            return 'NoSISymbolError'
        } else if (!unitDefinition.special && unitDefinition.SI && unitDefinition.symbol !== undefined) {
            return unitDefinition.symbol
        } else {
            return 'UnitIsNotSIUnitError'
        }
    }

    private getUnitPlural(unit: Unit, unitDefinition: Unit['units'][number]): string {
        if (!unitDefinition.special && unitDefinition.SI && unitDefinition.plural === undefined) {
            this.reportErrorSIUnitMissingPlural(unit, unitDefinition)
            return 'NoSIPluralError'
        } else if (!unitDefinition.special && unitDefinition.SI && unitDefinition.plural !== undefined) {
            return parseUnitPlural(unitDefinition.name, unitDefinition.plural)
        } else {
            return 'UnitIsNotSIUnitError'
        }
    }

    private reportErrorDocumentationFileNotFound(unit: Unit, fileName: string): void {
        console.error('Could not locate documentation file for unit: [' + unit.name + '], tried: [' + fileName + '].')
    }

    private reportErrorUnitExpressionParsing(unit: Unit, unitDefinition: Unit['units'][number]): void {
        console.error('Could not parse unit expression: [' + unit.name + ']' + (unitDefinition.special ? '.' : ', [' + unitDefinition.name + '].'))
    }

    private reportErrorNoSIUnit(unit: Unit): void {
        console.error('Could not identify SI unit of unit: [' + unit.name + ']. SI unit is defined using the JSON-entry {SI}.')
    }

    private reportErrorSIUnitMissingSymbol(unit: Unit, unitDefinition: Unit['units'][number]): void {
        console.error('SI unit of unit did not define a symbol: [' + unit.name + ']' + (unitDefinition.special ? '.' : ', [' + unitDefinition.name + '].') + ' Symbol is defined using the JSON-entry {symbol}.')
    }

    private reportErrorSIUnitMissingPlural(unit: Unit, unitDefinition: Unit['units'][number]): void {
        console.error('SI unit of unit did not define plural: [' + unit.name  + ']' + (unitDefinition.special ? '.' : ', [' + unitDefinition.name + '].') + ' Plural is defined using the JSON-entry {plural}.')
    }
}