﻿namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SpecializedSharpMeasuresVectorProperty<TPropertyType>
    : AttributeProperty<RawSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorLocations, TPropertyType>
{
    public SpecializedSharpMeasuresVectorProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SpecializedSharpMeasuresVectorProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public SpecializedSharpMeasuresVectorProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public SpecializedSharpMeasuresVectorProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}