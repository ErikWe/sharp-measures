namespace SharpMeasures.Generators.Parsing.Quantities.QuantityProcessAttribute;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <inheritdoc cref="IRawQuantityProcess"/>
internal sealed record class RawQuantityProcess : IRawQuantityProcess
{
    private ITypeSymbol Result { get; }
    private string? Name { get; }
    private string? Expression { get; }
    private IReadOnlyList<ITypeSymbol?>? Signature { get; }
    private IReadOnlyList<string?>? ParameterNames { get; }
    private bool? ImplementStatically { get; }

    private IQuantityProcessSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawQuantityProcess"/>, representing a parsed <see cref="QuantityProcessAttribute{TResult}"/>.</summary>
    /// <param name="result"><inheritdoc cref="IRawQuantityProcess.Result" path="/summary"/></param>
    /// <param name="name"><inheritdoc cref="IRawQuantityProcess.Name" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="IRawQuantityProcess.Expression" path="/summary"/></param>
    /// <param name="signature"><inheritdoc cref="IRawQuantityProcess.Signature" path="/summary"/></param>
    /// <param name="parameterNames"><inheritdoc cref="IRawQuantityProcess.ParameterNames" path="/summary"/></param>
    /// <param name="implementStatically"><inheritdoc cref="IRawQuantityProcess.ImplementStatically" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawQuantityProcess.Syntax" path="/summary"/></param>
    public RawQuantityProcess(ITypeSymbol result, string? name, string? expression, IReadOnlyList<ITypeSymbol?>? signature, IReadOnlyList<string?>? parameterNames, bool? implementStatically, IQuantityProcessSyntax? syntax)
    {
        Result = result;
        Name = name;
        Expression = expression;
        Signature = signature;
        ParameterNames = parameterNames;
        ImplementStatically = implementStatically;

        Syntax = syntax;
    }

    ITypeSymbol IRawQuantityProcess.Result => Result;
    string? IRawQuantityProcess.Name => Name;
    string? IRawQuantityProcess.Expression => Expression;
    IReadOnlyList<ITypeSymbol?>? IRawQuantityProcess.Signature => Signature;
    IReadOnlyList<string?>? IRawQuantityProcess.ParameterNames => ParameterNames;
    bool? IRawQuantityProcess.ImplementStatically => ImplementStatically;

    IQuantityProcessSyntax? IRawQuantityProcess.Syntax => Syntax;
}
