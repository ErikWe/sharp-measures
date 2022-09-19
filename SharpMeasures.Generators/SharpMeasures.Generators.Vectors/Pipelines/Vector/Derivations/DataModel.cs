namespace SharpMeasures.Generators.Vectors.Pipelines.Derivations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }
    public int Dimension { get; }

    public bool HasDefinedScalar { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations { get; }

    public IResolvedScalarPopulation ScalarPopulation { get; }

    public IVectorDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType vector, int dimension, bool hasDefinedScalar, IReadOnlyList<IDerivedQuantity> derivations, IResolvedScalarPopulation scalarPopulation, IVectorDocumentationStrategy documentation)
    {
        Vector = vector;
        Dimension = dimension;

        HasDefinedScalar = hasDefinedScalar;

        Derivations = derivations.AsReadOnlyEquatable();

        ScalarPopulation = scalarPopulation;

        Documentation = documentation;
    }
}
