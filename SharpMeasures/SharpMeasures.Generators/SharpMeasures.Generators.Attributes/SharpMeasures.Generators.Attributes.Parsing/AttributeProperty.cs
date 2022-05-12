namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Globalization;

public readonly record struct AttributeProperty<T>(string Name, string ParameterName, Func<T, object?, T> Setter,
    Func<T, AttributeArgumentListSyntax, int, T> SyntaxSetter)
{
    public AttributeProperty(string name, Func<T, object?, T> setter) : this(name, setter, static (x, _, _) => x) { }
    public AttributeProperty(string name, Func<T, object?, T> setter, Func<T, AttributeArgumentListSyntax, int, T> syntaxSetter)
        : this(name, ToParameterName(name ?? string.Empty), setter, syntaxSetter) { }
    public AttributeProperty(string name, string parameterName, Func<T, object?, T> setter) : this(name, parameterName, setter, static (x, _, _) => x) { }

    private static string ToParameterName(string propertyName) => propertyName.Substring(0, 1).ToLower(CultureInfo.CurrentCulture) + propertyName.Substring(1);
}