namespace SharpMeasures.Generators.Parsing.Scalars.SpecializedScalarQuantityAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IRawSpecializedScalarQuantity"/>
internal sealed record class RawSpecializedScalarQuantity : IRawSpecializedScalarQuantity
{
    private ITypeSymbol Original { get; }
    private bool? AllowNegative { get; }

    private bool? InheritOperations { get; }
    private bool? InheritProcesses { get; }
    private bool? InheritProperties { get; }
    private bool? InheritConstants { get; }
    private bool? InheritConversions { get; }

    private ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; }
    private ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; }

    private bool? ImplementSum { get; }
    private bool? ImplementDifference { get; }

    private ISpecializedScalarQuantitySyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawSpecializedScalarQuantity"/>, representing a parsed <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="original"><inheritdoc cref="IRawSpecializedScalarQuantity.Original" path="/summary"/></param>
    /// <param name="allowNegative"><inheritdoc cref="IRawSpecializedScalarQuantity.AllowNegative" path="/summary"/></param>
    /// <param name="inheritOperations"><inheritdoc cref="IRawSpecializedScalarQuantity.InheritOperations" path="/summary"/></param>
    /// <param name="inheritProcesses"><inheritdoc cref="IRawSpecializedScalarQuantity.InheritProcesses" path="/summary"/></param>
    /// <param name="inheritProperties"><inheritdoc cref="IRawSpecializedScalarQuantity.InheritProperties" path="/summary"/></param>
    /// <param name="inheritConstants"><inheritdoc cref="IRawSpecializedScalarQuantity.InheritConstants" path="/summary"/></param>
    /// <param name="inheritConversions"><inheritdoc cref="IRawSpecializedScalarQuantity.InheritConversions" path="/summary"/></param>
    /// <param name="forwardsCastOperatorBehaviour"><inheritdoc cref="IRawSpecializedScalarQuantity.ForwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="backwardsCastOperatorBehaviour"><inheritdoc cref="IRawSpecializedScalarQuantity.BackwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IRawSpecializedScalarQuantity.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IRawSpecializedScalarQuantity.ImplementDifference" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawSpecializedScalarQuantity.Syntax" path="/summary"/></param>
    public RawSpecializedScalarQuantity(ITypeSymbol original, bool? allowNegative, bool? inheritOperations, bool? inheritProcesses, bool? inheritProperties, bool? inheritConstants, bool? inheritConversions,
        ConversionOperatorBehaviour? forwardsCastOperatorBehaviour, ConversionOperatorBehaviour? backwardsCastOperatorBehaviour, bool? implementSum, bool? implementDifference, ISpecializedScalarQuantitySyntax? syntax)
    {
        Original = original;
        AllowNegative = allowNegative;

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

    ITypeSymbol IRawSpecializedScalarQuantity.Original => Original;
    bool? IRawSpecializedScalarQuantity.AllowNegative => AllowNegative;

    bool? IRawSpecializedScalarQuantity.InheritOperations => InheritOperations;
    bool? IRawSpecializedScalarQuantity.InheritProcesses => InheritProcesses;
    bool? IRawSpecializedScalarQuantity.InheritProperties => InheritProperties;
    bool? IRawSpecializedScalarQuantity.InheritConstants => InheritConstants;
    bool? IRawSpecializedScalarQuantity.InheritConversions => InheritConversions;

    ConversionOperatorBehaviour? IRawSpecializedScalarQuantity.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
    ConversionOperatorBehaviour? IRawSpecializedScalarQuantity.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;

    bool? IRawSpecializedScalarQuantity.ImplementSum => ImplementSum;
    bool? IRawSpecializedScalarQuantity.ImplementDifference => ImplementDifference;

    ISpecializedScalarQuantitySyntax? IRawSpecializedScalarQuantity.Syntax => Syntax;
}
