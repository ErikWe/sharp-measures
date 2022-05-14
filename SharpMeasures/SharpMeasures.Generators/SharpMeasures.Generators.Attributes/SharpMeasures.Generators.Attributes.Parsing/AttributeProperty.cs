namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Globalization;

public readonly record struct AttributeProperty<T>(string Name, string ParameterName, AttributeProperty<T>.DSetter Setter,
    AttributeProperty<T>.DSyntaxSetter SyntaxSetter)
{
    public delegate T DSetter(T definition, object? obj);
    public delegate T DSyntaxSetter(T definition, AttributeArgumentListSyntax argumentList, int index);

    public AttributeProperty(string name, DSetter setter, DSyntaxSetter syntaxSetter) : this(name, ToParameterName(name ?? string.Empty), setter, syntaxSetter) { }

    private static string ToParameterName(string propertyName) => propertyName.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + propertyName.Substring(1);
}