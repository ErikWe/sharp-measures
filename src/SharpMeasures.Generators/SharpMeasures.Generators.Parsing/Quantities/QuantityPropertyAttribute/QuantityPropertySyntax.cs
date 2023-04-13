namespace SharpMeasures.Generators.Parsing.Quantities.QuantityPropertyAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IQuantityPropertySyntax"/>
internal sealed record class QuantityPropertySyntax : IQuantityPropertySyntax
{
    private Location Result { get; }
    private Location Name { get; }
    private Location Expression { get; }
    private Location ImplementStatically { get; }

    /// <summary>Instantiates a <see cref="QuantityPropertySyntax"/>, representing syntactical information about a parsed <see cref="QuantityPropertyAttribute{TResult}"/>.</summary>
    /// <param name="result"><inheritdoc cref="IQuantityPropertySyntax.Result" path="/summary"/></param>
    /// <param name="name"><inheritdoc cref="IQuantityPropertySyntax.Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="IQuantityPropertySyntax.Expression" path="/summary"/></param>
    /// <param name="implementStatically"><inheritdoc cref="IQuantityPropertySyntax.ImplementStatically" path="/summary"/></param>
    public QuantityPropertySyntax(Location result, Location name, Location expression, Location implementStatically)
    {
        Result = result;
        Name = name;
        Expression = expression;
        ImplementStatically = implementStatically;
    }

    Location IQuantityPropertySyntax.Result => Result;
    Location IQuantityPropertySyntax.Name => Name;
    Location IQuantityPropertySyntax.Expression => Expression;
    Location IQuantityPropertySyntax.ImplementStatically => ImplementStatically;
}
