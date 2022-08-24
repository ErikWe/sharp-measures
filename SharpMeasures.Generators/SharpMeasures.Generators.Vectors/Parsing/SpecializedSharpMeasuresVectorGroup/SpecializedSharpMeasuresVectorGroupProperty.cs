﻿namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SpecializedSharpMeasuresVectorGroupProperty<TPropertyType>
    : AttributeProperty<RawSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupLocations, TPropertyType>
{
    public SpecializedSharpMeasuresVectorGroupProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SpecializedSharpMeasuresVectorGroupProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public SpecializedSharpMeasuresVectorGroupProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SpecializedSharpMeasuresVectorGroupProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}