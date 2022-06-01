namespace SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Processing;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public ProcessedDimensionalEquivalence DimensionallyEquivalentScalars { get; }

    public DocumentationFile Documentation { get; }

    public DataModel(DefinedType scalar, ProcessedDimensionalEquivalence dimensionallyEquivalentScalars, DocumentationFile documentation)
    {
        Scalar = scalar;
        DimensionallyEquivalentScalars = dimensionallyEquivalentScalars;
        Documentation = documentation;
    }

    public bool Equals(DataModel other)
    {
        return Scalar == other.Scalar && Documentation.Equals(other.Documentation)
            && DimensionallyEquivalentScalars == other.DimensionallyEquivalentScalars;
    }

    public override int GetHashCode()
    {
        return (Scalar, Documentation, DimensionallyEquivalentScalars).GetHashCode();
    }
}