namespace SharpMeasures.Generators.Parsing.Vectors.SpecializedVectorQuantityAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IRawSpecializedVectorQuantity"/>
internal sealed record class RawSpecializedVectorQuantity : IRawSpecializedVectorQuantity
{
    private ITypeSymbol Original { get; }

    private bool? InheritOperations { get; }
    private bool? InheritProcesses { get; }
    private bool? InheritProperties { get; }
    private bool? InheritConstants { get; }
    private bool? InheritConversions { get; }

    private ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; }
    private ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; }

    private bool? ImplementSum { get; }
    private bool? ImplementDifference { get; }

    private ISpecializedVectorQuantitySyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawSpecializedVectorQuantity"/>, representing a parsed <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="original"><inheritdoc cref="IRawSpecializedVectorQuantity.Original" path="/summary"/></param>
    /// <param name="inheritOperations"><inheritdoc cref="IRawSpecializedVectorQuantity.InheritOperations" path="/summary"/></param>
    /// <param name="inheritProcesses"><inheritdoc cref="IRawSpecializedVectorQuantity.InheritProcesses" path="/summary"/></param>
    /// <param name="inheritProperties"><inheritdoc cref="IRawSpecializedVectorQuantity.InheritProperties" path="/summary"/></param>
    /// <param name="inheritConstants"><inheritdoc cref="IRawSpecializedVectorQuantity.InheritConstants" path="/summary"/></param>
    /// <param name="inheritConversions"><inheritdoc cref="IRawSpecializedVectorQuantity.InheritConversions" path="/summary"/></param>
    /// <param name="forwardsCastOperatorBehaviour"><inheritdoc cref="IRawSpecializedVectorQuantity.ForwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="backwardsCastOperatorBehaviour"><inheritdoc cref="IRawSpecializedVectorQuantity.BackwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IRawSpecializedVectorQuantity.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IRawSpecializedVectorQuantity.ImplementDifference" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawSpecializedVectorQuantity.Syntax" path="/summary"/></param>
    public RawSpecializedVectorQuantity(ITypeSymbol original, bool? inheritOperations, bool? inheritProcesses, bool? inheritProperties, bool? inheritConstants, bool? inheritConversions,
        ConversionOperatorBehaviour? forwardsCastOperatorBehaviour, ConversionOperatorBehaviour? backwardsCastOperatorBehaviour, bool? implementSum, bool? implementDifference, ISpecializedVectorQuantitySyntax? syntax)
    {
        Original = original;

        InheritOperations = inheritOperations;
        InheritProcesses = inheritProcesses;
        InheritProperties = inheritProperties;
        InheritConstants = inheritConstants;
        InheritConversions = inheritConversions;

        ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
        BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Syntax = syntax;
    }

    ITypeSymbol IRawSpecializedVectorQuantity.Original => Original;

    bool? IRawSpecializedVectorQuantity.InheritOperations => InheritOperations;
    bool? IRawSpecializedVectorQuantity.InheritProcesses => InheritProcesses;
    bool? IRawSpecializedVectorQuantity.InheritProperties => InheritProperties;
    bool? IRawSpecializedVectorQuantity.InheritConstants => InheritConstants;
    bool? IRawSpecializedVectorQuantity.InheritConversions => InheritConversions;

    ConversionOperatorBehaviour? IRawSpecializedVectorQuantity.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
    ConversionOperatorBehaviour? IRawSpecializedVectorQuantity.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;

    bool? IRawSpecializedVectorQuantity.ImplementSum => ImplementSum;
    bool? IRawSpecializedVectorQuantity.ImplementDifference => ImplementDifference;

    ISpecializedVectorQuantitySyntax? IRawSpecializedVectorQuantity.Syntax => Syntax;
}
