namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;

internal record class ParsedScalar
{
    public DefinedType ScalarType { get; }
    public MinimalLocation ScalarLocation { get; }
    public GeneratedScalarDefinition ScalarDefinition { get; }

    public EquatableEnumerable<IncludeBasesDefinition> IncludeBases { get; }
    public EquatableEnumerable<ExcludeBasesDefinition> ExcludeBases { get; }

    public EquatableEnumerable<IncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<ExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<ScalarConstantDefinition> ScalarConstants { get; }
    public EquatableEnumerable<DimensionalEquivalenceDefinition> DimensionalEquivalences { get; }

    public ParsedScalar(DefinedType scalarType, MinimalLocation scalarLocation, GeneratedScalarDefinition scalarDefinition,
        IEnumerable<IncludeBasesDefinition> includeBases, IEnumerable<ExcludeBasesDefinition> excludeBases, IEnumerable<IncludeUnitsDefinition> includeUnits,
        IEnumerable<ExcludeUnitsDefinition> excludeUnits, IEnumerable<ScalarConstantDefinition> scalarConstants,
        IEnumerable<DimensionalEquivalenceDefinition> dimensionalEquivalences)
    {
        ScalarType = scalarType;
        ScalarLocation = scalarLocation;
        ScalarDefinition = scalarDefinition;

        IncludeBases = includeBases.AsEquatable();
        ExcludeBases = excludeBases.AsEquatable();

        IncludeUnits = includeUnits.AsEquatable();
        ExcludeUnits = excludeUnits.AsEquatable();

        ScalarConstants = scalarConstants.AsEquatable();
        DimensionalEquivalences = dimensionalEquivalences.AsEquatable();
    }
}
