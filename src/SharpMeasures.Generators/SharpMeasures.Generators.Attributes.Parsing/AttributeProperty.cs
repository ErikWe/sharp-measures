namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Globalization;

public interface IAttributeProperty
{
    public abstract string Name { get; }
    public abstract string ParameterName { get; }
}

public interface IAttributeProperty<TDefinition> : IAttributeProperty
{
    public delegate TDefinition DSetter(TDefinition definition, object? obj);
    public delegate TDefinition DLocator(TDefinition definition, AttributeArgumentListSyntax argumentList, int index, bool defenitelyParams = false);
    public delegate TDefinition DExplicitSetter(TDefinition definition);

    public abstract DSetter Setter { get; }
    public abstract DLocator Locator { get; }

    public abstract DExplicitSetter ExplicitSetter { get; }
}

public record class AttributeProperty<TDefinition> : IAttributeProperty<TDefinition>
{
    public delegate TDefinition DSingleLocator(TDefinition definition, MinimalLocation location);
    public delegate TDefinition DMultiLocator(TDefinition definition, MinimalLocation collection, IReadOnlyList<MinimalLocation> elements);

    public string Name { get; init; }
    public string ParameterName { get; init; }

    public IAttributeProperty<TDefinition>.DSetter Setter { get; init; }
    public IAttributeProperty<TDefinition>.DLocator Locator { get; init; }

    public IAttributeProperty<TDefinition>.DExplicitSetter ExplicitSetter { get; init; }

    private AttributeProperty(string name, string parameterName, IAttributeProperty<TDefinition>.DSetter setter, IAttributeProperty<TDefinition>.DLocator locator, IAttributeProperty<TDefinition>.DExplicitSetter explicitSetter)
    {
        Name = name;
        ParameterName = parameterName;

        Setter = setter;
        Locator = locator;

        ExplicitSetter = explicitSetter;
    }

    public AttributeProperty(string name, string parameterName, IAttributeProperty<TDefinition>.DSetter setter, DSingleLocator locator, IAttributeProperty<TDefinition>.DExplicitSetter explicitSetter) : this(name, parameterName, setter, WrapLocator(locator), explicitSetter) { }
    public AttributeProperty(string name, string parameterName, IAttributeProperty<TDefinition>.DSetter setter, DMultiLocator locator, IAttributeProperty<TDefinition>.DExplicitSetter explicitSetter) : this(name, parameterName, setter, WrapLocator(locator), explicitSetter) { }
    public AttributeProperty(string name, IAttributeProperty<TDefinition>.DSetter setter, DSingleLocator locator, IAttributeProperty<TDefinition>.DExplicitSetter explicitSetter) : this(name, ToParameterName(name), setter, locator, explicitSetter) { }
    public AttributeProperty(string name, IAttributeProperty<TDefinition>.DSetter setter, DMultiLocator locator, IAttributeProperty<TDefinition>.DExplicitSetter explicitSetter) : this(name, ToParameterName(name), setter, locator, explicitSetter) { }

    private static IAttributeProperty<TDefinition>.DLocator WrapLocator(DSingleLocator locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, AttributeArgumentListSyntax argumentList, int index, bool _) => locator(definition, ArgumentLocator.SimpleArgument(argumentList, index));
    }

    private static IAttributeProperty<TDefinition>.DLocator WrapLocator(DMultiLocator locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, AttributeArgumentListSyntax argumentList, int index, bool definitelyParams)
        {
            (var collection, var elements) = ArgumentLocator.FromArrayOrParamsList(argumentList, index, definitelyParams);

            return locator(definition, collection, elements);
        }
    }

    private static string ToParameterName(string propertyName) => propertyName.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + propertyName.Substring(1);
}

public record class AttributeProperty<TDefinition, TPropertyType> : AttributeProperty<TDefinition>
{
    public delegate TDefinition DTypeSetter(TDefinition definition, TPropertyType value);

    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DSingleLocator locator, IAttributeProperty<TDefinition>.DExplicitSetter explicitSetter) : base(name, parameterName, WrapSetter(setter), locator, explicitSetter) { }
    public AttributeProperty(string name, DTypeSetter setter, DSingleLocator locator, IAttributeProperty<TDefinition>.DExplicitSetter explicitSetter) : base(name, WrapSetter(setter), locator, explicitSetter) { }
    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DMultiLocator locator, IAttributeProperty<TDefinition>.DExplicitSetter explicitSetter) : base(name, parameterName, WrapSetter(setter), locator, explicitSetter) { }
    public AttributeProperty(string name, DTypeSetter setter, DMultiLocator locator, IAttributeProperty<TDefinition>.DExplicitSetter explicitSetter) : base(name, WrapSetter(setter), locator, explicitSetter) { }

    private static IAttributeProperty<TDefinition>.DSetter WrapSetter(DTypeSetter setter)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, object? obj)
        {
            if (typeof(TPropertyType).IsEnum && obj is int)
            {
                return setter(definition, (TPropertyType)obj);
            }

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
                var values = Array.CreateInstance(propertyType.GetElementType(), objArray.Length);

                for (var i = 0; i < objArray.Length; i++)
                {
                    if (objArray[i] is null)
                    {
                        try
                        {
                            values.SetValue(null, i);
                        }
                        catch (ArgumentException)
                        {
                            return definition;
                        }

                        continue;
                    }

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
    where TDefinition : IOpenAttributeDefinition<TDefinition, TLocations>
    where TLocations : IAttributeLocations
{
    public delegate TLocations DSingleLocationSetter(TLocations locations, MinimalLocation location);
    public delegate TLocations DMultiLocationSetter(TLocations locations, MinimalLocation collectionLocation, IReadOnlyList<MinimalLocation> elementLocations);
    public delegate TLocations DExplicitSetter(TLocations locations);

    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator, DExplicitSetter explicitSetter) : base(name, parameterName, setter, WrapLocator(locator), WrapExplicitSetter(explicitSetter)) { }
    public AttributeProperty(string name, DTypeSetter setter, DSingleLocationSetter locator, DExplicitSetter explicitSetter) : base(name, setter, WrapLocator(locator), WrapExplicitSetter(explicitSetter)) { }
    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator, DExplicitSetter explicitSetter) : base(name, parameterName, setter, WrapLocator(locator), WrapExplicitSetter(explicitSetter)) { }
    public AttributeProperty(string name, DTypeSetter setter, DMultiLocationSetter locator, DExplicitSetter explicitSetter) : base(name, setter, WrapLocator(locator), WrapExplicitSetter(explicitSetter)) { }

    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DSingleLocationSetter locator) : base(name, parameterName, setter, WrapLocator(locator), MarkExplicitAsNoLocation(locator)) { }
    public AttributeProperty(string name, DTypeSetter setter, DSingleLocationSetter locator) : base(name, setter, WrapLocator(locator), MarkExplicitAsNoLocation(locator)) { }
    public AttributeProperty(string name, string parameterName, DTypeSetter setter, DMultiLocationSetter locator) : base(name, parameterName, setter, WrapLocator(locator), MarkExplicitAsNoLocation(locator)) { }
    public AttributeProperty(string name, DTypeSetter setter, DMultiLocationSetter locator) : base(name, setter, WrapLocator(locator), MarkExplicitAsNoLocation(locator)) { }

    private static DSingleLocator WrapLocator(DSingleLocationSetter locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, MinimalLocation location)
        {
            var modifiedLocations = locator(definition.Locations, location);

            return definition.WithLocations(modifiedLocations);
        }
    }

    private static DMultiLocator WrapLocator(DMultiLocationSetter locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition, MinimalLocation collectionLocation, IReadOnlyList<MinimalLocation> elementLocations)
        {
            var modifiedLocations = locator(definition.Locations, collectionLocation, elementLocations);

            return definition.WithLocations(modifiedLocations);
        }
    }

    private static IAttributeProperty<TDefinition>.DExplicitSetter WrapExplicitSetter(DExplicitSetter explicitSetter)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition)
        {
            var modifiedLocations = explicitSetter(definition.Locations);

            return definition.WithLocations(modifiedLocations);
        }
    }

    private static IAttributeProperty<TDefinition>.DExplicitSetter MarkExplicitAsNoLocation(DSingleLocationSetter locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition)
        {
            var modifiedLocations = locator(definition.Locations, MinimalLocation.None);

            return definition.WithLocations(modifiedLocations);
        }
    }

    private static IAttributeProperty<TDefinition>.DExplicitSetter MarkExplicitAsNoLocation(DMultiLocationSetter locator)
    {
        return wrapper;

        TDefinition wrapper(TDefinition definition)
        {
            var modifiedLocations = locator(definition.Locations, MinimalLocation.None, Array.Empty<MinimalLocation>());

            return definition.WithLocations(modifiedLocations);
        }
    }
}
