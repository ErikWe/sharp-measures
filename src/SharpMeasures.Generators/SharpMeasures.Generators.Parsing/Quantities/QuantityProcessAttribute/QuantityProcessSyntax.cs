namespace SharpMeasures.Generators.Parsing.Quantities.QuantityProcessAttribute;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <inheritdoc cref="IQuantityProcessSyntax"/>
internal sealed record class QuantityProcessSyntax : IQuantityProcessSyntax
{
    private Location Result { get; }
    private Location Name { get; }
    private Location Expression { get; }
    private Location SignatureCollection { get; }
    private IReadOnlyList<Location> SignatureElements { get; }
    private Location ParameterNamesCollection { get; }
    private IReadOnlyList<Location> ParameterNamesElements { get; }
    private Location ImplementStatically { get; }

    /// <summary>Instantiates a <see cref="QuantityProcessSyntax"/>, representing syntactical information about a parsed <see cref="QuantityProcessAttribute{TResult}"/>.</summary>
    /// <param name="result"><inheritdoc cref="IQuantityProcessSyntax.Result" path="/summary"/></param>
    /// <param name="name"><inheritdoc cref="IQuantityProcessSyntax.Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="IQuantityProcessSyntax.Expression" path="/summary"/></param>
    /// <param name="signatureCollection"><inheritdoc cref="IQuantityProcessSyntax.SignatureCollection" path="/summary"/></param>
    /// <param name="signatureElements"><inheritdoc cref="IQuantityProcessSyntax.SignatureElements" path="/summary"/></param>
    /// <param name="parameterNamesCollection"><inheritdoc cref="IQuantityProcessSyntax.ParameterNamesCollection" path="/summary"/></param>
    /// <param name="parameterNamesElements"><inheritdoc cref="IQuantityProcessSyntax.ParameterNamesElements" path="/summary"/></param>
    /// <param name="implementStatically"><inheritdoc cref="IQuantityProcessSyntax.ImplementStatically" path="/summary"/></param>
    public QuantityProcessSyntax(Location result, Location name, Location expression, Location signatureCollection, IReadOnlyList<Location> signatureElements, Location parameterNamesCollection, IReadOnlyList<Location> parameterNamesElements, Location implementStatically)
    {
        Result = result;
        Name = name;
        Expression = expression;
        SignatureCollection = signatureCollection;
        SignatureElements = signatureElements;
        ParameterNamesCollection = parameterNamesCollection;
        ParameterNamesElements = parameterNamesElements;
        ImplementStatically = implementStatically;
    }

    Location IQuantityProcessSyntax.Result => Result;
    Location IQuantityProcessSyntax.Name => Name;
    Location IQuantityProcessSyntax.Expression => Expression;
    Location IQuantityProcessSyntax.SignatureCollection => SignatureCollection;
    IReadOnlyList<Location> IQuantityProcessSyntax.SignatureElements => SignatureElements;
    Location IQuantityProcessSyntax.ParameterNamesCollection => ParameterNamesCollection;
    IReadOnlyList<Location> IQuantityProcessSyntax.ParameterNamesElements => ParameterNamesElements;
    Location IQuantityProcessSyntax.ImplementStatically => ImplementStatically;
}
