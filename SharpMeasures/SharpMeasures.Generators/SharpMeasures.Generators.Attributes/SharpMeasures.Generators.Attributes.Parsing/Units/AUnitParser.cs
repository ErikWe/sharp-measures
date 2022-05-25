namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System;
using System.Collections.Generic;

internal abstract class AUnitParser<TDefinition, TParsingData, TLocations, TAttribute> : AAttributeParser<TDefinition, TParsingData, TLocations, TAttribute>
    where TDefinition : AUnitDefinition<TParsingData, TLocations>
    where TParsingData : AUnitParsingData<TLocations>
    where TLocations : AUnitLocations
{
    protected AUnitParser(Func<TDefinition> defaultValueConstructor, IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> constructorParameters,
        IReadOnlyDictionary<string, IAttributeProperty<TDefinition>> namedParameters)
        : base(defaultValueConstructor, constructorParameters, namedParameters) { }

    protected AUnitParser(Func<TDefinition> defaultValueConstructor, IEnumerable<IAttributeProperty<TDefinition>> properties)
        : base(defaultValueConstructor, properties) { }

    protected override TDefinition AddCustomData(TDefinition definition, AttributeData attributeData, AttributeSyntax attributeSyntax)
    {
        var modifiedParsingData = definition.ParsingData with { InterpretedPlural = SimpleTextExpression.Interpret(definition.Name, definition.Plural) };
        definition = definition with { ParsingData = modifiedParsingData };

        return base.AddCustomData(definition, attributeData, attributeSyntax);
    }
}
