namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Equatables;

using System.Collections.Generic;

internal record class ParsedScalar
{
    public DefinedType ScalarType { get; }
    public MinimalLocation ScalarLocation { get; }
    public GeneratedScalar ScalarDefinition { get; }

    public EquatableEnumerable<IncludeBases> IncludeBases { get; }
    public EquatableEnumerable<ExcludeBases> ExcludeBases { get; }

    public EquatableEnumerable<IncludeUnits> IncludeUnits { get; }
    public EquatableEnumerable<ExcludeUnits> ExcludeUnits { get; }

    public EquatableEnumerable<ScalarConstant> ScalarConstants { get; }
    public EquatableEnumerable<DimensionalEquivalence> DimensionalEquivalences { get; }

    public ParsedScalar(DefinedType scalarType, MinimalLocation scalarLocation, GeneratedScalar scalarDefinition,
        IEnumerable<IncludeBases> includeBases, IEnumerable<ExcludeBases> excludeBases,
        IEnumerable<IncludeUnits> includeUnits, IEnumerable<ExcludeUnits> excludeUnits,
        IEnumerable<ScalarConstant> scalarConstants, IEnumerable<DimensionalEquivalence> dimensionalEquivalences)
    {
        ScalarType = scalarType;
        ScalarLocation = scalarLocation;
        ScalarDefinition = scalarDefinition;

        IncludeBases = new(includeBases);
        ExcludeBases = new(excludeBases);

        IncludeUnits = new(includeUnits);
        ExcludeUnits = new(excludeUnits);

        ScalarConstants = new(scalarConstants);
        DimensionalEquivalences = new(dimensionalEquivalences);
    }
}
