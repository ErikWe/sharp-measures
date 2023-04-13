namespace SharpMeasures.Generators.Parsing.Units.DerivableUnitAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IRawDerivableUnit"/>
internal sealed record class RawDerivableUnit : IRawDerivableUnit
{
    private string? DerivationID { get; }
    private string? Expression { get; }
    private IReadOnlyList<ITypeSymbol?>? Signature { get; }

    private IDerivableUnitSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawDerivableUnit"/>, representing a parsed <see cref="DerivableUnitAttribute"/>.</summary>
    /// <param name="derivationID"><inheritdoc cref="IRawDerivableUnit.DerivationID" path="/summary"/></param>
    /// <param name="expression"><inheritdoc cref="IRawDerivableUnit.Expression" path="/summary"/></param>
    /// <param name="signature"><inheritdoc cref="IRawDerivableUnit.Signature" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawDerivableUnit.Syntax" path="/summary"/></param>
    public RawDerivableUnit(string? derivationID, string? expression, IReadOnlyList<ITypeSymbol?>? signature, IDerivableUnitSyntax? syntax)
    {
        DerivationID = derivationID;
        Expression = expression;
        Signature = signature;

        Syntax = syntax;
    }

    string? IRawDerivableUnit.DerivationID => DerivationID;
    string? IRawDerivableUnit.Expression => Expression;
    IReadOnlyList<ITypeSymbol?>? IRawDerivableUnit.Signature => Signature;

    IDerivableUnitSyntax? IRawDerivableUnit.Syntax => Syntax;
}
