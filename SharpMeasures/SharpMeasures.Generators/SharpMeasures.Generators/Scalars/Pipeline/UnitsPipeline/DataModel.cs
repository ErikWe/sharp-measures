namespace SharpMeasures.Generators.Scalars.Pipeline.UnitsPipeline;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct DataModel(DefinedType Scalar, CacheableGeneratedScalarDefinition Definition, IReadOnlyList<UnitName> UnitNames,
    CacheableIncludeBasesDefinition? IncludedBases, CacheableExcludeBasesDefinition? ExcludedBases,
    CacheableIncludeUnitsDefinition? IncludedUnits, CacheableExcludeUnitsDefinition? ExcludedUnits, DocumentationFile Documentation)
{
    public bool Equals(DataModel other)
    {
        return Scalar == other.Scalar && Definition == other.Definition && UnitNames.SequenceEqual(other.UnitNames) && IncludedBases == other.IncludedBases
            && ExcludedBases == other.ExcludedBases && IncludedUnits == other.IncludedUnits && ExcludedUnits == other.ExcludedUnits
            && Documentation.Equals(other.Documentation);
    }

    public override int GetHashCode()
    {
        int hashCode = (Scalar, Definition, IncludedBases, ExcludedBases, IncludedUnits, ExcludedUnits, Documentation).GetHashCode();

        foreach (UnitName unitName in UnitNames)
        {
            hashCode ^= unitName.GetHashCode();
        }

        return hashCode;
    }
}