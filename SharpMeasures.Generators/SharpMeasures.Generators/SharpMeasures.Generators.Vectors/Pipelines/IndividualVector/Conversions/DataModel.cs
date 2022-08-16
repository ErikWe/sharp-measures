namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Conversions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Groups;
using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public int Dimension { get; }

    public IReadOnlyList<IConvertibleVector> Conversions => conversions;
    public IReadOnlyDictionary<NamedType, IVectorGroupType> VectorGroupPopulation => vectorGroupPopulation;

    public IIndividualVectorDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IConvertibleVector> conversions { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupType> vectorGroupPopulation { get; }

    public DataModel(DefinedType vector, int dimension, IReadOnlyList<IConvertibleVector> conversions, IReadOnlyDictionary<NamedType, IVectorGroupType> vectorGroupPopulation,
        IIndividualVectorDocumentationStrategy documentation)
    {
        Vector = vector;

        Dimension = dimension;

        this.conversions = conversions.AsReadOnlyEquatable();
        this.vectorGroupPopulation = vectorGroupPopulation.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
