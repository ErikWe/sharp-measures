namespace SharpMeasures.Generators.Units.Pipeline.Derivable;

using SharpMeasures.Generators.Documentation;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, DocumentationFile Documentation,
    IEnumerable<ExtendedDerivableUnitDefinition> Derivations)
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
