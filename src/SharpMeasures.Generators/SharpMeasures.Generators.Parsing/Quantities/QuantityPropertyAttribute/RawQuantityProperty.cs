namespace SharpMeasures.Generators.Parsing.Quantities.QuantityPropertyAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IRawQuantityProperty"/>
internal sealed record class RawQuantityProperty : IRawQuantityProperty
{
    private ITypeSymbol Result { get; }
    private string? Name { get; }
    private string? Expression { get; }
    private bool? ImplementStatically { get; }

    private IQuantityPropertySyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawQuantityProperty"/>, representing a parsed <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
    /// <param name="result"><inheritdoc cref="IRawQuantityProperty.Result" path="/summary"/></param>
    /// <param name="name"><inheritdoc cref="IRawQuantityProperty.Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="IRawQuantityProperty.Expression" path="/summary"/></param>
    /// <param name="implementStatically"><inheritdoc cref="IRawQuantityProperty.ImplementStatically" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawQuantityProperty.Syntax" path="/summary"/></param>
    public RawQuantityProperty(ITypeSymbol result, string? name, string? expression, bool? implementStatically, IQuantityPropertySyntax? syntax)
    {
        Result = result;
        Name = name;
        Expression = expression;
        ImplementStatically = implementStatically;

        Syntax = syntax;
    }

    ITypeSymbol IRawQuantityProperty.Result => Result;
    string? IRawQuantityProperty.Name => Name;
    string? IRawQuantityProperty.Expression => Expression;
    bool? IRawQuantityProperty.ImplementStatically => ImplementStatically;

    IQuantityPropertySyntax? IRawQuantityProperty.Syntax => Syntax;
}
