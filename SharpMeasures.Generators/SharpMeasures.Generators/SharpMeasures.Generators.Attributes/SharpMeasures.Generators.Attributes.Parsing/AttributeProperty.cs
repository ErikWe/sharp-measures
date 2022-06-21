namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Equatables;

using System;
using System.Globalization;

public interface IAttributeProperty
{
    public abstract string Name { get; }
    public abstract string ParameterName { get; }
}

public interface IAttributeProperty<TDefinition> : IAttributeProperty
{
    public delegate TDefinition DSetter(TDefinition definition, object? obj);
    public delegate TDefinition DLocator(TDefinition definition, AttributeArgumentListSyntax argumentList, int index);

    public abstract DSetter Setter { get; }
    public abstract DLocator Locator { get; }
}

public record class AttributeProperty<TDefinition> : IAttributeProperty<TDefinition>
{
    public delegate TDefinition DSingleLocator(TDefinition definition, MinimalLocation location);
    public delegate TDefinition DMultiLocator(TDefinition definition, MinimalLocation collection, ReadOnlyEquatableList<MinimalLocation> elements);

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

            return locator(definition, collection, elements.AsReadOnlyEquatable());
        }
    }

    private static string ToParameterName(string propertyName) => propertyName.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + propertyName.Substring(1);
}

public record class AttributeProperty<TDefinition, TPropertyType> : AttributeProperty<TDefinition>
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
                if (obj is object[] objArray)
                {
                    return arrayWrapper(definition, objArray);
                }

                return definition;
            }

            return setter(definition, value);
        }

        TDefinition arrayWrapper(TDefinition definition, object[] objArray)
        {
            if (typeof(TPropertyType) is Type { IsArray: true } propertyType)
            {
                Array values = Array.CreateInstance(propertyType.GetElementType(), objArray.Length);

                for (int i = 0; i < objArray.Length; i++)
                {
                    if (propertyType.GetElementType().IsAssignableFrom(objArray[i].GetType()) is false)
                    {
                        return definition;
                    }

                    values.SetValue(objArray[i], i);
                }

                return setter(definition, (TPropertyType)(object)values);
            }

            return definition;
        }
    }
}

public record class AttributeProperty<TDefinition, TLocations, TPropertyType> : AttributeProperty<TDefinition, TPropertyType>
    where TDefinition : ARawAttributeDefinition<TLocations>
    where TLocations : AAttributeLocations
{
    public delegate TLocations DSingleLocationSetter(TLocations definition, MinimalLocation location);
    public delegate TLocations DMultiLocationSetter(TLocations definition, MinimalLocation collectionLocation, ReadOnlyEquatableList<MinimalLocation> elementLocations);

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
            var modifiedLocations = locator(definition.Locations, location);

            return definition with { Locations = modifiedLocations };
        }
    }

    private static DMultiLocator WrapLocator(DMultiLocationSetter locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, MinimalLocation collectionLocation, ReadOnlyEquatableList<MinimalLocation> elementLocations)
        {
            var modifiedLocations = locator(definition.Locations, collectionLocation, elementLocations);

            return definition with { Locations = modifiedLocations };
        }
    }
}
