namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Processing;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct DataModel(DefinedType Scalar, NamedType Unit, NamedType UnitQuantity, IReadOnlyCollection<UnitInstance> Bases,
    IReadOnlyCollection<UnitInstance> Units, IReadOnlyList<ProcessedScalarConstant> Constants, DocumentationFile Documentation)
{
    public bool Equals(DataModel other)
    {
        return Scalar == other.Scalar && Unit == other.Unit && Bases.SequenceEqual(other.Bases) && Units.SequenceEqual(other.Units)
            && Constants.SequenceEqual(other.Constants) && Documentation.Equals(other.Documentation);
    }

    public override int GetHashCode()
    {
        return (Scalar, Unit, Documentation).GetHashCode() ^ Bases.GetSequenceHashCode() ^ Units.GetSequenceHashCode() ^ Constants.GetSequenceHashCode();
    }
}