namespace SharpMeasures.Generators.Parsing.Vectors.SpecializedVectorGroupAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IRawSpecializedVectorGroup"/>
internal sealed record class RawSpecializedVectorGroup : IRawSpecializedVectorGroup
{
    private ITypeSymbol Original { get; }

    private bool? InheritOperations { get; }
    private bool? InheritConversions { get; }

    private ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; }
    private ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; }

    private bool? ImplementSum { get; }
    private bool? ImplementDifference { get; }

    private ISpecializedVectorGroupSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawSpecializedVectorGroup"/>, representing a parsed <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    /// <param name="original"><inheritdoc cref="IRawSpecializedVectorGroup.Original" path="/summary"/></param>
    /// <param name="inheritOperations"><inheritdoc cref="IRawSpecializedVectorGroup.InheritOperations" path="/summary"/></param>
    /// <param name="inheritConversions"><inheritdoc cref="IRawSpecializedVectorGroup.InheritConversions" path="/summary"/></param>
    /// <param name="forwardsCastOperatorBehaviour"><inheritdoc cref="IRawSpecializedVectorGroup.ForwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="backwardsCastOperatorBehaviour"><inheritdoc cref="IRawSpecializedVectorGroup.BackwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IRawSpecializedVectorGroup.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IRawSpecializedVectorGroup.ImplementDifference" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawSpecializedVectorGroup.Syntax" path="/summary"/></param>
    public RawSpecializedVectorGroup(ITypeSymbol original, bool? inheritOperations, bool? inheritConversions, ConversionOperatorBehaviour? forwardsCastOperatorBehaviour,
        ConversionOperatorBehaviour? backwardsCastOperatorBehaviour, bool? implementSum, bool? implementDifference, ISpecializedVectorGroupSyntax? syntax)
    {
        Original = original;

        InheritOperations = inheritOperations;
        InheritConversions = inheritConversions;

        ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
        BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Syntax = syntax;
    }

    ITypeSymbol IRawSpecializedVectorGroup.Original => Original;

    bool? IRawSpecializedVectorGroup.InheritOperations => InheritOperations;
    bool? IRawSpecializedVectorGroup.InheritConversions => InheritConversions;

    ConversionOperatorBehaviour? IRawSpecializedVectorGroup.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
    ConversionOperatorBehaviour? IRawSpecializedVectorGroup.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;

    bool? IRawSpecializedVectorGroup.ImplementSum => ImplementSum;
    bool? IRawSpecializedVectorGroup.ImplementDifference => ImplementDifference;

    ISpecializedVectorGroupSyntax? IRawSpecializedVectorGroup.Syntax => Syntax;
}
