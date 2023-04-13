namespace SharpMeasures.Generators.Parsing.Vectors.VectorGroupAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IVectorGroupSyntax"/>
internal sealed record class VectorGroupSyntax : IVectorGroupSyntax
{
    private Location Unit { get; }
    private Location ImplementSum { get; }
    private Location ImplementDifference { get; }

    /// <summary>Instantiates a <see cref="VectorGroupSyntax"/>, representing syntactical information about a parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="unit"><inheritdoc cref="IVectorGroupSyntax.Unit" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IVectorGroupSyntax.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IVectorGroupSyntax.ImplementDifference" path="/summary"/></param>
    public VectorGroupSyntax(Location unit, Location implementSum, Location implementDifference)
    {
        Unit = unit;
        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
    }

    Location IVectorGroupSyntax.Unit => Unit;
    Location IVectorGroupSyntax.ImplementSum => ImplementSum;
    Location IVectorGroupSyntax.ImplementDifference => ImplementDifference;

}
