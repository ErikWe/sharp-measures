namespace SharpMeasures.Generators.Parsing.Scalars.SpecializedUnitlessQuantityAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IRawSpecializedUnitlessQuantity"/>
internal sealed record class RawSpecializedUnitlessQuantity : IRawSpecializedUnitlessQuantity
{
    private ITypeSymbol Original { get; }
    private bool? AllowNegative { get; }

    private bool? InheritOperations { get; }
    private bool? InheritProcesses { get; }
    private bool? InheritProperties { get; }
    private bool? InheritConversions { get; }

    private ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; }
    private ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; }

    private bool? ImplementSum { get; }
    private bool? ImplementDifference { get; }

    private ISpecializedUnitlessQuantitySyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawSpecializedUnitlessQuantity"/>, representing a parsed <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="original"><inheritdoc cref="IRawSpecializedUnitlessQuantity.Original" path="/summary"/></param>
    /// <param name="allowNegative"><inheritdoc cref="IRawSpecializedUnitlessQuantity.AllowNegative" path="/summary"/></param>
    /// <param name="inheritOperations"><inheritdoc cref="IRawSpecializedUnitlessQuantity.InheritOperations" path="/summary"/></param>
    /// <param name="inheritProcesses"><inheritdoc cref="IRawSpecializedUnitlessQuantity.InheritProcesses" path="/summary"/></param>
    /// <param name="inheritProperties"><inheritdoc cref="IRawSpecializedUnitlessQuantity.InheritProperties" path="/summary"/></param>
    /// <param name="inheritConversions"><inheritdoc cref="IRawSpecializedUnitlessQuantity.InheritConversions" path="/summary"/></param>
    /// <param name="forwardsCastOperatorBehaviour"><inheritdoc cref="IRawSpecializedUnitlessQuantity.ForwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="backwardsCastOperatorBehaviour"><inheritdoc cref="IRawSpecializedUnitlessQuantity.BackwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IRawSpecializedUnitlessQuantity.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IRawSpecializedUnitlessQuantity.ImplementDifference" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawSpecializedUnitlessQuantity.Syntax" path="/summary"/></param>
    public RawSpecializedUnitlessQuantity(ITypeSymbol original, bool? allowNegative, bool? inheritOperations, bool? inheritProcesses, bool? inheritProperties, bool? inheritConversions,
        ConversionOperatorBehaviour? forwardsCastOperatorBehaviour, ConversionOperatorBehaviour? backwardsCastOperatorBehaviour, bool? implementSum, bool? implementDifference, ISpecializedUnitlessQuantitySyntax? syntax)
    {
        Original = original;
        AllowNegative = allowNegative;

        InheritOperations = inheritOperations;
        InheritProcesses = inheritProcesses;
        InheritProperties = inheritProperties;
        InheritConversions = inheritConversions;

        ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
        BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Syntax = syntax;
    }

    ITypeSymbol IRawSpecializedUnitlessQuantity.Original => Original;
    bool? IRawSpecializedUnitlessQuantity.AllowNegative => AllowNegative;

    bool? IRawSpecializedUnitlessQuantity.InheritOperations => InheritOperations;
    bool? IRawSpecializedUnitlessQuantity.InheritProcesses => InheritProcesses;
    bool? IRawSpecializedUnitlessQuantity.InheritProperties => InheritProperties;
    bool? IRawSpecializedUnitlessQuantity.InheritConversions => InheritConversions;

    ConversionOperatorBehaviour? IRawSpecializedUnitlessQuantity.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
    ConversionOperatorBehaviour? IRawSpecializedUnitlessQuantity.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;

    bool? IRawSpecializedUnitlessQuantity.ImplementSum => ImplementSum;
    bool? IRawSpecializedUnitlessQuantity.ImplementDifference => ImplementDifference;

    ISpecializedUnitlessQuantitySyntax? IRawSpecializedUnitlessQuantity.Syntax => Syntax;
}
