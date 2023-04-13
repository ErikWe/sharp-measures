namespace SharpMeasures.Generators.Parsing.Scalars.SpecializedUnitlessQuantityAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="ISpecializedUnitlessQuantitySyntax"/>
internal sealed record class SpecializedUnitlessQuantitySyntax : ISpecializedUnitlessQuantitySyntax
{
    private Location Original { get; }
    private Location AllowNegative { get; }
    private Location InheritOperations { get; }
    private Location InheritProcesses { get; }
    private Location InheritProperties { get; }
    private Location InheritConversions { get; }
    private Location ForwardsCastOperatorBehaviour { get; }
    private Location BackwardsCastOperatorBehaviour { get; }
    private Location ImplementSum { get; }
    private Location ImplementDifference { get; }

    /// <summary>Instantiates a <see cref="SpecializedUnitlessQuantitySyntax"/>, representing syntactical information about a parsed <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="original"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.Original" path="/summary"/></param>
    /// <param name="allowNegative"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.AllowNegative" path="/summary"/></param>
    /// <param name="inheritOperations"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.InheritOperations" path="/summary"/></param>
    /// <param name="inheritProcesses"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.InheritProcesses" path="/summary"/></param>
    /// <param name="inheritProperties"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.InheritProperties" path="/summary"/></param>
    /// <param name="inheritConversions"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.InheritConversions" path="/summary"/></param>
    /// <param name="forwardsCastOperatorBehaviour"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.ForwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="backwardsCastOperatorBehaviour"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.BackwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="ISpecializedUnitlessQuantitySyntax.ImplementDifference" path="/summary"/></param>
    public SpecializedUnitlessQuantitySyntax(Location original, Location allowNegative, Location inheritOperations, Location inheritProcesses, Location inheritProperties,
        Location inheritConversions, Location forwardsCastOperatorBehaviour, Location backwardsCastOperatorBehaviour, Location implementSum, Location implementDifference)
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
    }

    Location ISpecializedUnitlessQuantitySyntax.Original => Original;
    Location ISpecializedUnitlessQuantitySyntax.AllowNegative => AllowNegative;
    Location ISpecializedUnitlessQuantitySyntax.InheritOperations => InheritOperations;
    Location ISpecializedUnitlessQuantitySyntax.InheritProcesses => InheritProcesses;
    Location ISpecializedUnitlessQuantitySyntax.InheritProperties => InheritProperties;
    Location ISpecializedUnitlessQuantitySyntax.InheritConversions => InheritConversions;
    Location ISpecializedUnitlessQuantitySyntax.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
    Location ISpecializedUnitlessQuantitySyntax.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;
    Location ISpecializedUnitlessQuantitySyntax.ImplementSum => ImplementSum;
    Location ISpecializedUnitlessQuantitySyntax.ImplementDifference => ImplementDifference;
}
