namespace SharpMeasures.Generators.Scalars.Pipelines.Derivations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IOperatorDerivation> OperatorDerivations => operatorDerivations;

    public IResolvedScalarPopulation ScalarPopulation { get; }

    public IDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IOperatorDerivation> operatorDerivations { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IOperatorDerivation> operatorDerivations, IResolvedScalarPopulation scalarPopulation, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.operatorDerivations = operatorDerivations.AsReadOnlyEquatable();

        ScalarPopulation = scalarPopulation;

        Documentation = documentation;
    }
}
