namespace SharpMeasures.Generators.Parsing.Vectors.VectorQuantityAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IVectorQuantitySyntax"/>
internal sealed record class VectorQuantitySyntax : IVectorQuantitySyntax
{
    private Location Unit { get; }
    private Location Dimension { get; }
    private Location ImplementSum { get; }
    private Location ImplementDifference { get; }

    /// <summary>Instantiates a <see cref="VectorQuantitySyntax"/>, representing syntactical information about a parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="unit"><inheritdoc cref="IVectorQuantitySyntax.Unit" path="/summary"/></param>
    /// <param name="dimension"><inheritdoc cref="IVectorQuantitySyntax.Dimension" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IVectorQuantitySyntax.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IVectorQuantitySyntax.ImplementDifference" path="/summary"/></param>
    public VectorQuantitySyntax(Location unit, Location dimension, Location implementSum, Location implementDifference)
    {
        Unit = unit;
        Dimension = dimension;
        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
    }

    Location IVectorQuantitySyntax.Unit => Unit;
    Location IVectorQuantitySyntax.Dimension => Dimension;
    Location IVectorQuantitySyntax.ImplementSum => ImplementSum;
    Location IVectorQuantitySyntax.ImplementDifference => ImplementDifference;

}
