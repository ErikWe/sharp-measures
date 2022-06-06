namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

internal record class ResizedVectorProperty<TPropertyType>
    : AttributeProperty<RawResizedVector, ResizedVectorLocations, TPropertyType>
{
    public ResizedVectorProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ResizedVectorProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, locator) { }
    public ResizedVectorProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, locator) { }
    public ResizedVectorProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, locator) { }
}