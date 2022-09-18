namespace SharpMeasures.Generators.Scalars.Pipelines.Derivations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;

    public IResolvedScalarPopulation ScalarPopulation { get; }

    public IDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IDerivedQuantity> derivations, IResolvedScalarPopulation scalarPopulation, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        this.derivations = derivations.AsReadOnlyEquatable();

        ScalarPopulation = scalarPopulation;

        Documentation = documentation;
    }
}
