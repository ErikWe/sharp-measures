namespace SharpMeasures.Generators.Parsing.Scalars.SpecializedScalarQuantityAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="ISpecializedScalarQuantitySyntax"/>
internal sealed record class SpecializedScalarQuantitySyntax : ISpecializedScalarQuantitySyntax
{
    private Location Original { get; }
    private Location AllowNegative { get; }
    private Location InheritOperations { get; }
    private Location InheritProcesses { get; }
    private Location InheritProperties { get; }
    private Location InheritConstants { get; }
    private Location InheritConversions { get; }
    private Location ForwardsCastOperatorBehaviour { get; }
    private Location BackwardsCastOperatorBehaviour { get; }
    private Location ImplementSum { get; }
    private Location ImplementDifference { get; }

    /// <summary>Instantiates a <see cref="SpecializedScalarQuantitySyntax"/>, representing syntactical information about a parsed <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="original"><inheritdoc cref="ISpecializedScalarQuantitySyntax.Original" path="/summary"/></param>
    /// <param name="allowNegative"><inheritdoc cref="ISpecializedScalarQuantitySyntax.AllowNegative" path="/summary"/></param>
    /// <param name="inheritOperations"><inheritdoc cref="ISpecializedScalarQuantitySyntax.InheritOperations" path="/summary"/></param>
    /// <param name="inheritProcesses"><inheritdoc cref="ISpecializedScalarQuantitySyntax.InheritProcesses" path="/summary"/></param>
    /// <param name="inheritProperties"><inheritdoc cref="ISpecializedScalarQuantitySyntax.InheritProperties" path="/summary"/></param>
    /// <param name="inheritConstants"><inheritdoc cref="ISpecializedScalarQuantitySyntax.InheritConstants" path="/summary"/></param>
    /// <param name="inheritConversions"><inheritdoc cref="ISpecializedScalarQuantitySyntax.InheritConversions" path="/summary"/></param>
    /// <param name="forwardsCastOperatorBehaviour"><inheritdoc cref="ISpecializedScalarQuantitySyntax.ForwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="backwardsCastOperatorBehaviour"><inheritdoc cref="ISpecializedScalarQuantitySyntax.BackwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="ISpecializedScalarQuantitySyntax.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="ISpecializedScalarQuantitySyntax.ImplementDifference" path="/summary"/></param>
    public SpecializedScalarQuantitySyntax(Location original, Location allowNegative, Location inheritOperations, Location inheritProcesses, Location inheritProperties, Location inheritConstants,
        Location inheritConversions, Location forwardsCastOperatorBehaviour, Location backwardsCastOperatorBehaviour, Location implementSum, Location implementDifference)
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
    }

    Location ISpecializedScalarQuantitySyntax.Original => Original;
    Location ISpecializedScalarQuantitySyntax.AllowNegative => AllowNegative;
    Location ISpecializedScalarQuantitySyntax.InheritOperations => InheritOperations;
    Location ISpecializedScalarQuantitySyntax.InheritProcesses => InheritProcesses;
    Location ISpecializedScalarQuantitySyntax.InheritProperties => InheritProperties;
    Location ISpecializedScalarQuantitySyntax.InheritConstants => InheritConstants;
    Location ISpecializedScalarQuantitySyntax.InheritConversions => InheritConversions;
    Location ISpecializedScalarQuantitySyntax.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
    Location ISpecializedScalarQuantitySyntax.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;
    Location ISpecializedScalarQuantitySyntax.ImplementSum => ImplementSum;
    Location ISpecializedScalarQuantitySyntax.ImplementDifference => ImplementDifference;
}
