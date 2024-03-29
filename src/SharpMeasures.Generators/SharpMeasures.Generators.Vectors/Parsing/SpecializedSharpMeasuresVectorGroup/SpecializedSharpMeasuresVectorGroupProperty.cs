﻿namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SpecializedSharpMeasuresVectorGroupProperty<TPropertyType> : AttributeProperty<SymbolicSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupLocations, TPropertyType>
{
    public SpecializedSharpMeasuresVectorGroupProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
}
