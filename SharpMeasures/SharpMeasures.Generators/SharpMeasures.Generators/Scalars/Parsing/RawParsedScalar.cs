namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Equatables;

using System.Collections.Generic;

internal record class RawParsedScalar
{
    public DefinedType ScalarType { get; }
    public MinimalLocation ScalarLocation { get; }
    public GeneratedScalar ScalarDefinition { get; }

    public EquatableEnumerable<RawIncludeBases> IncludeBases { get; }
    public EquatableEnumerable<RawExcludeBases> ExcludeBases { get; }

    public EquatableEnumerable<RawIncludeUnits> IncludeUnits { get; }
    public EquatableEnumerable<RawExcludeUnits> ExcludeUnits { get; }

    public EquatableEnumerable<RawScalarConstant> ScalarConstants { get; }

    public EquatableEnumerable<RawDimensionalEquivalence> DimensionalEquivalences { get; }

    public RawParsedScalar(DefinedType scalarType, MinimalLocation scalarLocation, GeneratedScalar scalarDefinition,
        IEnumerable<RawIncludeBases> includeBases, IEnumerable<RawExcludeBases> excludeBases,
        IEnumerable<RawIncludeUnits> includeUnits, IEnumerable<RawExcludeUnits> excludeUnits,
        IEnumerable<RawScalarConstant> scalarConstants, IEnumerable<RawDimensionalEquivalence> dimensionalEquivalences)
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
