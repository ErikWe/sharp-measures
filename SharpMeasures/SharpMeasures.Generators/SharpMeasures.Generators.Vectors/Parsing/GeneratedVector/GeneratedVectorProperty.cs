﻿namespace SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class GeneratedVectorProperty<TPropertyType>
    : AttributeProperty<RawGeneratedVectorDefinition, GeneratedVectorLocations, TPropertyType>
{
    public GeneratedVectorProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedVectorProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public GeneratedVectorProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public GeneratedVectorProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}