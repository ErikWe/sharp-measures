﻿namespace SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class GeneratedUnitParser
{
    public static IAttributeParser<RawGeneratedVectorDefinition> Parser { get; } = new AttributeParser();

    private static RawGeneratedVectorDefinition DefaultDefiniton() => RawGeneratedVectorDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawGeneratedVectorDefinition, GeneratedVectorLocations, GeneratedVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedVectorProperties.AllProperties) { }
    }
}
