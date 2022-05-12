namespace SharpMeasures.Generators.Units.Pipeline.DerivableUnitPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System;
using System.Collections.Generic;
using System.Linq;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, Location UnitLocation,
    IReadOnlyList<CacheableDerivableUnitDefinition> DefinedDerivations, DocumentationFile Documentation)
{
    public bool Equals(DataModel other)
    {
        return Unit == other.Unit && Quantity == other.Quantity && UnitLocation == other.UnitLocation && Documentation == other.Documentation
            && DefinedDerivations.SequenceEqual(other.DefinedDerivations);
    }

    public override int GetHashCode()
    {
        int hashCode = (Unit, Quantity, UnitLocation, Documentation).GetHashCode();

        foreach (CacheableDerivableUnitDefinition definition in DefinedDerivations)
        {
            hashCode ^= definition.GetHashCode();
        }

        return hashCode;
    }
}
