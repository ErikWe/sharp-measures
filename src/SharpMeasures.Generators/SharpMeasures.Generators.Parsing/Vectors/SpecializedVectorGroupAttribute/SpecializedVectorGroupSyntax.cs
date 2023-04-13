namespace SharpMeasures.Generators.Parsing.Vectors.SpecializedVectorGroupAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="ISpecializedVectorGroupSyntax"/>
internal sealed record class SpecializedVectorGroupSyntax : ISpecializedVectorGroupSyntax
{
    private Location Original { get; }
    private Location InheritOperations { get; }
    private Location InheritConversions { get; }
    private Location ForwardsCastOperatorBehaviour { get; }
    private Location BackwardsCastOperatorBehaviour { get; }
    private Location ImplementSum { get; }
    private Location ImplementDifference { get; }

    /// <summary>Instantiates a <see cref="SpecializedVectorGroupSyntax"/>, representing syntactical information about a parsed <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    /// <param name="original"><inheritdoc cref="ISpecializedVectorGroupSyntax.Original" path="/summary"/></param>
    /// <param name="inheritOperations"><inheritdoc cref="ISpecializedVectorGroupSyntax.InheritOperations" path="/summary"/></param>
    /// <param name="inheritConversions"><inheritdoc cref="ISpecializedVectorGroupSyntax.InheritConversions" path="/summary"/></param>
    /// <param name="forwardsCastOperatorBehaviour"><inheritdoc cref="ISpecializedVectorGroupSyntax.ForwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="backwardsCastOperatorBehaviour"><inheritdoc cref="ISpecializedVectorGroupSyntax.BackwardsCastOperatorBehaviour" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="ISpecializedVectorGroupSyntax.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="ISpecializedVectorGroupSyntax.ImplementDifference" path="/summary"/></param>
    public SpecializedVectorGroupSyntax(Location original, Location inheritOperations, Location inheritConversions, Location forwardsCastOperatorBehaviour, Location backwardsCastOperatorBehaviour,
        Location implementSum, Location implementDifference)
    {
        Original = original;

        InheritOperations = inheritOperations;
        InheritConversions = inheritConversions;

        ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
        BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
    }

    Location ISpecializedVectorGroupSyntax.Original => Original;
    Location ISpecializedVectorGroupSyntax.InheritOperations => InheritOperations;
    Location ISpecializedVectorGroupSyntax.InheritConversions => InheritConversions;
    Location ISpecializedVectorGroupSyntax.ForwardsCastOperatorBehaviour => ForwardsCastOperatorBehaviour;
    Location ISpecializedVectorGroupSyntax.BackwardsCastOperatorBehaviour => BackwardsCastOperatorBehaviour;
    Location ISpecializedVectorGroupSyntax.ImplementSum => ImplementSum;
    Location ISpecializedVectorGroupSyntax.ImplementDifference => ImplementDifference;
}
