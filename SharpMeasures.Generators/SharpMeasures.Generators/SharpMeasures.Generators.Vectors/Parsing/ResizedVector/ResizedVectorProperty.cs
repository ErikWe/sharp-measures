﻿namespace SharpMeasures.Generators.Vectors.Parsing.ResizedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class ResizedVectorProperty<TPropertyType>
    : AttributeProperty<RawResizedVectorDefinition, ResizedVectorLocations, TPropertyType>
{
    public ResizedVectorProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ResizedVectorProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ResizedVectorProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ResizedVectorProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}