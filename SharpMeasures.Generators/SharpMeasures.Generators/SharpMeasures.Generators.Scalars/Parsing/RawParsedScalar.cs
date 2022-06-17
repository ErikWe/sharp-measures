﻿namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;

internal record class RawParsedScalar
{
    public DefinedType ScalarType { get; }
    public MinimalLocation ScalarLocation { get; }
    public SharpMeasuresScalarDefinition ScalarDefinition { get; }

    public EquatableEnumerable<RawIncludeBasesDefinition> IncludeBases { get; }
    public EquatableEnumerable<RawExcludeBasesDefinition> ExcludeBases { get; }

    public EquatableEnumerable<RawIncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<RawExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<RawScalarConstantDefinition> ScalarConstants { get; }

    public EquatableEnumerable<RawDimensionalEquivalenceDefinition> DimensionalEquivalences { get; }

    public RawParsedScalar(DefinedType scalarType, MinimalLocation scalarLocation, SharpMeasuresScalarDefinition scalarDefinition,
        IEnumerable<RawIncludeBasesDefinition> includeBases, IEnumerable<RawExcludeBasesDefinition> excludeBases,
        IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits,
        IEnumerable<RawScalarConstantDefinition> scalarConstants, IEnumerable<RawDimensionalEquivalenceDefinition> dimensionalEquivalences)
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
