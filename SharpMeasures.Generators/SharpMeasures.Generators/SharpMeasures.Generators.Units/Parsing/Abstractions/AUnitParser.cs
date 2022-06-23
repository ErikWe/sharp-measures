namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

internal abstract class AUnitParser<TDefinition, TParsingData, TLocations, TAttribute> : AAttributeParser<TDefinition, TLocations, TAttribute>
    where TDefinition : ARawUnitDefinition<TParsingData, TLocations>
    where TParsingData : AUnitParsingData
    where TLocations : AUnitLocations
{
    protected AUnitParser(Func<TDefinition> defaultValueConstructor, IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> constructorParameters,
        IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> namedParameters)
        : base(defaultValueConstructor, constructorParameters, namedParameters) { }

    protected AUnitParser(Func<TDefinition> defaultValueConstructor, IEnumerable<IAttributeProperty<TDefinition>> properties)
        : base(defaultValueConstructor, properties) { }

    protected override TDefinition AddCustomData(TDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax,
        ImmutableArray<IParameterSymbol> parameterSymbols)
    {
        if (definition.Name is null || definition.Plural is null)
        {
            return definition;
        }

        var modifiedParsingData = definition.ParsingData with { InterpretedPlural = SimpleTextExpression.Interpret(definition.Name, definition.Plural) };
        definition = definition with { ParsingData = modifiedParsingData };

        return base.AddCustomData(definition, attributeData, attributeSyntax, parameterSymbols);
    }
}
