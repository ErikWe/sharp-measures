namespace SharpMeasures.Generators.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

/// <summary>Allows the arguments of an attribute to be parsed syntactically, resulting in a <typeparamref name="T"/> if successful.</summary>
/// <typeparam name="T">The type that describes the parsed attribute.</typeparam>
public interface IConstructiveSyntacticAttributeParser<out T>
{
    /// <summary>Attempts to parse the arguments of an argument described by the provided <see cref="AttributeData"/>.</summary>
    /// <param name="attributeData">The <see cref="AttributeData"/> describing the attribute.</param>
    /// <param name="attributeSyntax">The <see cref="AttributeSyntax"/>, syntactically describing how the attribute was used.</param>
    /// <returns>The parsed <typeparamref name="T"/>, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="InvalidOperationException"/>
    public abstract T? TryParse(AttributeData attributeData, AttributeSyntax attributeSyntax);
}
