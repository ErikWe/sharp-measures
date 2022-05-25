namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;
using System.Globalization;

internal interface IAttributeProperty
{
    public abstract string Name { get; }
    public abstract string ParameterName { get; }
}

internal interface IAttributeProperty<TDefinition> : IAttributeProperty
{
    public delegate TDefinition DSetter(TDefinition definition, object? obj);
    public delegate TDefinition DLocator(TDefinition definition, AttributeArgumentListSyntax argumentList, int index);

    public abstract DSetter Setter { get; }
    public abstract DLocator Locator { get; }
}

internal record class AttributeProperty<TDefinition> : IAttributeProperty<TDefinition>
{
    public delegate TDefinition DSingleLocator(TDefinition definition, MinimalLocation location);
    public delegate TDefinition DMultiLocator(TDefinition definition, MinimalLocation collection, IReadOnlyList<MinimalLocation> elements);

    public string Name { get; init; }
    public string ParameterName { get; init; }

    public IAttributeProperty<TDefinition>.DSetter Setter { get; init; }
    public IAttributeProperty<TDefinition>.DLocator Locator { get; init; }

    private AttributeProperty(string name, string parameterName, IAttributeProperty<TDefinition>.DSetter setter, IAttributeProperty<TDefinition>.DLocator locator)
    {
        Name = name;
        ParameterName = parameterName;
        Setter = setter;
        Locator = locator;
    }

    public AttributeProperty(string name, string parameterName, IAttributeProperty<TDefinition>.DSetter setter, DSingleLocator locator)
        : this(name, parameterName, setter, WrapLocator(locator)) { }

    public AttributeProperty(string name, string parameterName, IAttributeProperty<TDefinition>.DSetter setter, DMultiLocator locator)
        : this(name, parameterName, setter, WrapLocator(locator)) { }

    public AttributeProperty(string name, IAttributeProperty<TDefinition>.DSetter setter, DSingleLocator locator) : this(name, ToParameterName(name), setter, locator) { }
    public AttributeProperty(string name, IAttributeProperty<TDefinition>.DSetter setter, DMultiLocator locator) : this(name, ToParameterName(name), setter, locator) { }

    private static IAttributeProperty<TDefinition>.DLocator WrapLocator(DSingleLocator locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, AttributeArgumentListSyntax argumentList, int index)
        {
            return locator(definition, ArgumentLocator.SimpleArgument(argumentList, index));
        }
    }

    private static IAttributeProperty<TDefinition>.DLocator WrapLocator(DMultiLocator locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, AttributeArgumentListSyntax argumentList, int index)
        {
            (var collection, var elements) = ArgumentLocator.FromArrayOrParamsList(argumentList, index);

            return locator(definition, collection, elements);
        }
    }

    private static string ToParameterName(string propertyName) => propertyName.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + propertyName.Substring(1);
}

internal record class AttributeProperty<TDefinition, TPropertyType> : AttributeProperty<TDefinition>
{
    public delegate TDefinition DTypeSetter(TDefinition definition, TPropertyType value);

    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DSingleLocator locator) : base(name, parameterName, WrapSetter(setter), locator) { }
    public AttributeProperty(string name, DTypeSetter setter, DSingleLocator locator) : base(name, WrapSetter(setter), locator) { }
    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DMultiLocator locator) : base(name, parameterName, WrapSetter(setter), locator) { }
    public AttributeProperty(string name, DTypeSetter setter, DMultiLocator locator) : base(name, WrapSetter(setter), locator) { }

    private static IAttributeProperty<TDefinition>.DSetter WrapSetter(DTypeSetter setter)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, object? obj)
        {
            if (obj is not TPropertyType value)
            {
                return definition;
            }

            return setter(definition, value);
        }
    }
}

internal record class AttributeProperty<TDefinition, TParsingData, TLocations, TPropertyType> : AttributeProperty<TDefinition, TPropertyType>
    where TDefinition : AAttributeDefinition<TParsingData, TLocations>
    where TParsingData : AAttributeParsingData<TLocations>
    where TLocations : AAttributeLocations
{
    public delegate TLocations DSingleLocationSetter(TLocations definition, MinimalLocation location);
    public delegate TLocations DMultiLocationSetter(TLocations definition, MinimalLocation collectionLocation, IReadOnlyList<MinimalLocation> elementLocations);

    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator)
        : base(name, parameterName, setter, WrapLocator(locator)) { }

    public AttributeProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, WrapLocator(locator)) { }

    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator)
        : base(name, parameterName, setter, WrapLocator(locator)) { }

    public AttributeProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, WrapLocator(locator)) { }

    private static DSingleLocator WrapLocator(DSingleLocationSetter locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, MinimalLocation location)
        {
            var modifiedLocations = locator(definition.ParsingData.Locations, location);
            var modifiedParsingData = definition.ParsingData with { Locations = modifiedLocations };

            return definition with { ParsingData = modifiedParsingData };
        }
    }

    private static DMultiLocator WrapLocator(DMultiLocationSetter locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, MinimalLocation collectionLocation, IReadOnlyList<MinimalLocation> elementLocations)
        {
            var modifiedLocations = locator(definition.ParsingData.Locations, collectionLocation, elementLocations);
            var modifiedParsingData = definition.ParsingData with { Locations = modifiedLocations };

            return definition with { ParsingData = modifiedParsingData };
        }
    }
}