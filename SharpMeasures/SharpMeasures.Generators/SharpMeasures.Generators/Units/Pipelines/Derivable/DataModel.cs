namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.Processing;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, DocumentationFile Documentation,
    IEnumerable<ProcessedDerivableUnit> Derivations)
{
    public bool Equals(DataModel other)
    {
        return Unit == other.Unit && Quantity == other.Quantity && Documentation.Equals(other.Documentation)
            && Derivations.SequenceEqual(other.Derivations);
    }

    public override int GetHashCode()
    {
        return (Unit, Quantity, Documentation).GetHashCode() ^ Derivations.GetSequenceHashCode();
    }
}
