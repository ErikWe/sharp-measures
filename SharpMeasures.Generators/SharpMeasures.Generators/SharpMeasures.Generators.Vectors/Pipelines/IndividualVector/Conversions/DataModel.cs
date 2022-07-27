namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Conversions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public int Dimension { get; }

    public IReadOnlyList<IConvertibleVector> Conversions => conversions;

    public IIndividualVectorDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IConvertibleVector> conversions { get; }

    public DataModel(DefinedType vector, int dimension, IReadOnlyList<IConvertibleVector> conversions, IIndividualVectorDocumentationStrategy documentation)
    {
        Vector = vector;

        Dimension = dimension;

        this.conversions = conversions.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
