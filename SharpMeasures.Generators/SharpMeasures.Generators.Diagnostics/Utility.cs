namespace SharpMeasures.Generators.Diagnostics;

using System;

internal static class Utility
{
    public static string ShortAttributeName<TAttribute>() => ShortAttributeName(typeof(TAttribute));
    public static string ShortAttributeName(Type attributeType) => AttributeName(attributeType.Name);
    public static string FullAttributeName<TAttribute>() => FullAttributeName(typeof(TAttribute));
    public static string FullAttributeName(Type attributeType) => AttributeName(attributeType.FullName);
    public static string AttributeName(string attributeName)
    {
        if (attributeName.EndsWith("Attribute", StringComparison.InvariantCulture))
        {
            return attributeName.Substring(0, attributeName.Length - 9);
        }

        return attributeName;
    }
}
