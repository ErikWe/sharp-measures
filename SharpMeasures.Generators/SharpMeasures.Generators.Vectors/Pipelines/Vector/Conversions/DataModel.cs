namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Conversions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public int Dimension { get; }

    public IResolvedVectorPopulation VectorPopulation { get; }

    public IReadOnlyList<IConvertibleVector> Conversions => conversions;

    public IVectorDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IConvertibleVector> conversions { get; }

    public DataModel(DefinedType vector, int dimension, IReadOnlyList<IConvertibleVector> conversions, IResolvedVectorPopulation vectorPopulation,
        IVectorDocumentationStrategy documentation)
    {
        Vector = vector;

        Dimension = dimension;

        VectorPopulation = vectorPopulation;

        this.conversions = conversions.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
