﻿namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresVectorGroupParser
{
    public static IAttributeParser<SymbolicSharpMeasuresVectorGroupDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSharpMeasuresVectorGroupDefinition DefaultDefiniton() => SymbolicSharpMeasuresVectorGroupDefinition.Empty;

    private class AttributeParser : AAttributeParser<SymbolicSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations, SharpMeasuresVectorGroupAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresVectorGroupProperties.AllProperties) { }
    }
}
