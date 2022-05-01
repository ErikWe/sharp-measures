namespace SharpMeasures.Generators.Attributes.Parsing;

using System;
using System.Globalization;

public readonly record struct AttributeProperty<T>(string Name, string ParameterName, Func<T, object?, T> Setter)
{
    public AttributeProperty(string name, Func<T, object?, T> setter) : this(name, ToParameterName(name ?? string.Empty), setter) { }

    private static string ToParameterName(string propertyName) => propertyName.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + propertyName.Substring(1);
}