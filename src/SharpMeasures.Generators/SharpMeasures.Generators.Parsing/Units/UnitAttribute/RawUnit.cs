namespace SharpMeasures.Generators.Parsing.Units.UnitAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IRawUnit"/>
internal sealed record class RawUnit : IRawUnit
{
    private ITypeSymbol Scalar { get; }
    private bool? BiasTerm { get; }

    private IUnitSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawUnit"/>, representing a parsed, unsanitized, <see cref="UnitAttribute{TScalar}"/>.</summary>
    /// <param name="scalar"><inheritdoc cref="IRawUnit.Scalar" path="/summary"/></param>
    /// <param name="biasTerm"><inheritdoc cref="IRawUnit.BiasTerm" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawUnit.Syntax" path="/summary"/></param>
    public RawUnit(ITypeSymbol scalar, bool? biasTerm, IUnitSyntax? syntax)
    {
        Scalar = scalar;
        BiasTerm = biasTerm;

        Syntax = syntax;
    }

    ITypeSymbol IRawUnit.Scalar => Scalar;
    bool? IRawUnit.BiasTerm => BiasTerm;

    IUnitSyntax? IRawUnit.Syntax => Syntax;
}
