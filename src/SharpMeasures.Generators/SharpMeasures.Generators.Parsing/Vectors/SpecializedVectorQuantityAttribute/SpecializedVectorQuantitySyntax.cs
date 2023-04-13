namespace SharpMeasures.Generators.Parsing.Vectors.SpecializedVectorQuantityAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="ISpecializedVectorQuantitySyntax"/>
internal sealed record class SpecializedVectorQuantitySyntax : ISpecializedVectorQuantitySyntax
{
    private Location Original { get; }
    private Location InheritOperations { get; }
    private Location InheritProcesses { get; }
    private Location InheritProperties { get; }
    private Location InheritConstants { get; }
    private Location InheritConversions { get; }
    private Location ForwardsCastOperatorBehaviour { get; }
    private Location BackwardsCastOperatorBehaviour { get; }
    private Location ImplementSum { get; }
    private Location ImplementDifference { get; }

    /// <summary>Instantiates a <see cref="SpecializedVectorQuantitySyntax"/>, representing syntactical information about a parsed <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="original"><inheritdoc cref="ISpecializedVectorQuantitySyntax.Original" path="/summary"/></param>
    /// <param name="inheritOperations"><inheritdoc cref="ISpecializedVectorQuantitySyntax.InheritOperations" path="/summary"/></param>
    /// <param name="inheritProcesses"><inheritdoc cref="ISpecializedVectorQuantitySyntax.InheritProcesses" path="/summary"/></param>
    /// <param name="inheritProperties"><inheritdoc cref="ISpecializedVectorQuantitySyntax.InheritProperties" path="/summary"/></param>
    /// <param name="inheritConstants"><inheritdoc cref="ISpecializedVectorQuantitySyntax.InheritConstants" path="/summary"/></param>
    /// <param name="inheritConversions"><inheritdoc cref="ISpecializedVectorQuantitySyntax.InheritConversions" path="/summary"/></param>
    /// <param name="forwardsCastOperatorBehaviour"><inheritdoc cref="ISpecializedVectorQuantitySyntax.ForwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="backwardsCastOperatorBehaviour"><inheritdoc cref="ISpecializedVectorQuantitySyntax.BackwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="ISpecializedVectorQuantitySyntax.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="ISpecializedVectorQuantitySyntax.ImplementDifference" path="/summary"/></param>
    public SpecializedVectorQuantitySyntax(Location original, Location inheritOperations, Location inheritProcesses, Location inheritProperties, Location inheritConstants,
        Location inheritConversions, Location forwardsCastOperatorBehaviour, Location backwardsCastOperatorBehaviour, Location implementSum, Location implementDifference)
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
    }

    Location ISpecializedVectorQuantitySyntax.Original => Original;
    Location ISpecializedVectorQuantitySyntax.InheritOperations => InheritOperations;
    Location ISpecializedVectorQuantitySyntax.InheritProcesses => InheritProcesses;
    Location ISpecializedVectorQuantitySyntax.InheritProperties => InheritProperties;
    Location ISpecializedVectorQuantitySyntax.InheritConstants => InheritConstants;
    Location ISpecializedVectorQuantitySyntax.InheritConversions => InheritConversions;
    Location ISpecializedVectorQuantitySyntax.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
    Location ISpecializedVectorQuantitySyntax.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;
    Location ISpecializedVectorQuantitySyntax.ImplementSum => ImplementSum;
    Location ISpecializedVectorQuantitySyntax.ImplementDifference => ImplementDifference;
}
