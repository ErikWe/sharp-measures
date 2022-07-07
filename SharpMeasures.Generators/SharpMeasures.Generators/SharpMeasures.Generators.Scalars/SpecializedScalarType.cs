namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Abstractions;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;

internal record class SpecializedScalarType : ISpecializedScalarType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public SpecializedSharpMeasuresScalarDefinition ScalarDefinition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;

    public IReadOnlyList<ScalarConstantDefinition> Constants => constants;

    public IReadOnlyList<IncludeBasesDefinition> IncludeBases => includeBases;
    public IReadOnlyList<ExcludeBasesDefinition> ExcludeBases => excludeBases;

    public IReadOnlyList<IncludeUnitsDefinition> IncludeUnits => includeUnits;
    public IReadOnlyList<ExcludeUnitsDefinition> ExcludeUnits => excludeUnits;

    public IReadOnlyList<ConvertibleQuantityDefinition> ConvertibleQuantities => convertibleQuantities;

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }

    private ReadOnlyEquatableList<ScalarConstantDefinition> constants { get; }

    private ReadOnlyEquatableList<IncludeBasesDefinition> includeBases { get; }
    private ReadOnlyEquatableList<ExcludeBasesDefinition> excludeBases { get; }

    private ReadOnlyEquatableList<IncludeUnitsDefinition> includeUnits { get; }
    private ReadOnlyEquatableList<ExcludeUnitsDefinition> excludeUnits { get; }

    private ReadOnlyEquatableList<ConvertibleQuantityDefinition> convertibleQuantities { get; }

    ISpecializedScalar ISpecializedScalarType.ScalarDefinition => ScalarDefinition;

    IReadOnlyList<IDerivedQuantity> ISpecializedScalarType.Derivations => Derivations;
    IReadOnlyList<IScalarConstant> ISpecializedScalarType.Constants => Constants;
    IReadOnlyList<IIncludeBases> ISpecializedScalarType.IncludeBases => IncludeBases;
    IReadOnlyList<IExcludeBases> ISpecializedScalarType.ExcludeBases => ExcludeBases;
    IReadOnlyList<IIncludeUnits> ISpecializedScalarType.IncludeUnits => IncludeUnits;
    IReadOnlyList<IExcludeUnits> ISpecializedScalarType.ExcludeUnits => ExcludeUnits;
    IReadOnlyList<IConvertibleQuantity> ISpecializedScalarType.ConvertibleQuantities => ConvertibleQuantities;

    public SpecializedScalarType(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresScalarDefinition scalarDefinition,
        IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<IncludeBasesDefinition> includeBases,
        IReadOnlyList<ExcludeBasesDefinition> excludeBases, IReadOnlyList<IncludeUnitsDefinition> includeUnits, IReadOnlyList<ExcludeUnitsDefinition> excludeUnits,
        IReadOnlyList<ConvertibleQuantityDefinition> convertibleQuantities)
    {
        Type = type;
        TypeLocation = typeLocation;
        ScalarDefinition = scalarDefinition;

        this.derivations = derivations.AsReadOnlyEquatable();

        this.constants = constants.AsReadOnlyEquatable();

        this.includeBases = includeBases.AsReadOnlyEquatable();
        this.excludeBases = excludeBases.AsReadOnlyEquatable();

        this.includeUnits = includeUnits.AsReadOnlyEquatable();
        this.excludeUnits = excludeUnits.AsReadOnlyEquatable();

        this.convertibleQuantities = convertibleQuantities.AsReadOnlyEquatable();
    }
}
