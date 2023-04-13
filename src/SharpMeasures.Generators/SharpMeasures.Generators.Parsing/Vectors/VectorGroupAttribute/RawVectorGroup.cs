namespace SharpMeasures.Generators.Parsing.Vectors.VectorGroupAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IRawVectorGroup"/>
internal sealed record class RawVectorGroup : IRawVectorGroup
{
    private ITypeSymbol Unit { get; }

    private bool? ImplementSum { get; }
    private bool? ImplementDifference { get; }

    private IVectorGroupSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawVectorGroup"/>, representing a parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="unit"><inheritdoc cref="IRawVectorGroup.Unit" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IRawVectorGroup.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IRawVectorGroup.ImplementDifference" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawVectorGroup.Syntax" path="/summary"/></param>
    public RawVectorGroup(ITypeSymbol unit, bool? implementSum, bool? implementDifference, IVectorGroupSyntax? syntax)
    {
        Unit = unit;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Syntax = syntax;
    }

    ITypeSymbol IRawVectorGroup.Unit => Unit;

    bool? IRawVectorGroup.ImplementSum => ImplementSum;
    bool? IRawVectorGroup.ImplementDifference => ImplementDifference;

    IVectorGroupSyntax? IRawVectorGroup.Syntax => Syntax;
}
