namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Abstractions;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;

internal record class RawBaseScalarType : IRawScalarType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public RawSharpMeasuresScalarDefinition ScalarDefinition { get; }

    public IEnumerable<RawDerivedQuantityDefinition> Derivations => derivations;

    public IEnumerable<RawScalarConstantDefinition> Constants => constants;

    public IEnumerable<RawIncludeBasesDefinition> IncludeBases => includeBases;
    public IEnumerable<RawExcludeBasesDefinition> ExcludeBases => excludeBases;

    public IEnumerable<RawIncludeUnitsDefinition> IncludeUnits => includeUnits;
    public IEnumerable<RawExcludeUnitsDefinition> ExcludeUnits => excludeUnits;

    public IEnumerable<RawConvertibleQuantityDefinition> ConvertibleQuantities => convertibleQuantities;

    public EquatableEnumerable<RawDerivedQuantityDefinition> derivations { get; }

    private EquatableEnumerable<RawScalarConstantDefinition> constants { get; }

    private EquatableEnumerable<RawIncludeBasesDefinition> includeBases { get; }
    private EquatableEnumerable<RawExcludeBasesDefinition> excludeBases { get; }

    private EquatableEnumerable<RawIncludeUnitsDefinition> includeUnits { get; }
    private EquatableEnumerable<RawExcludeUnitsDefinition> excludeUnits { get; }

    private EquatableEnumerable<RawConvertibleQuantityDefinition> convertibleQuantities { get; }

    public RawBaseScalarType(DefinedType scalarType, MinimalLocation scalarLocation, RawSharpMeasuresScalarDefinition scalarDefinition,
        IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawIncludeBasesDefinition> includeBases,
        IEnumerable<RawExcludeBasesDefinition> excludeBases, IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits,
        IEnumerable<RawConvertibleQuantityDefinition> convertibleQuantities)
    {
        Type = scalarType;
        TypeLocation = scalarLocation;
        ScalarDefinition = scalarDefinition;

        this.derivations = derivations.AsEquatable();

        this.constants = constants.AsEquatable();

        this.includeBases = includeBases.AsEquatable();
        this.excludeBases = excludeBases.AsEquatable();

        this.includeUnits = includeUnits.AsEquatable();
        this.excludeUnits = excludeUnits.AsEquatable();

        this.convertibleQuantities = convertibleQuantities.AsEquatable();
    }
}
