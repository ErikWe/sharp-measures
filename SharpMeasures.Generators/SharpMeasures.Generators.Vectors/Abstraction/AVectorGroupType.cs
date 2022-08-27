namespace SharpMeasures.Generators.Vectors.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal record class AVectorGroupType<TDefinition> : IVectorGroupType
    where TDefinition : IVectorGroup
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TDefinition Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<ConvertibleVectorDefinition> Conversions => conversions;

    public IReadOnlyList<IncludeUnitsDefinition> UnitInclusions => unitInclusions;
    public IReadOnlyList<ExcludeUnitsDefinition> UnitExclusions => unitExclusions;

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<ConvertibleVectorDefinition> conversions { get; }

    private ReadOnlyEquatableList<IncludeUnitsDefinition> unitInclusions { get; }
    private ReadOnlyEquatableList<ExcludeUnitsDefinition> unitExclusions { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IVectorGroup IVectorGroupType.Definition => Definition;

    IReadOnlyList<IDerivedQuantity> IQuantityType.Derivations => Derivations;
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;
    IReadOnlyList<IConvertibleVector> IVectorGroupType.Conversions => Conversions;

    IReadOnlyList<IUnitList> IQuantityType.UnitInclusions => UnitInclusions;
    IReadOnlyList<IUnitList> IQuantityType.UnitExclusions => UnitExclusions;

    protected AVectorGroupType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.unitInclusions = unitInclusions.AsReadOnlyEquatable();
        this.unitExclusions = unitExclusions.AsReadOnlyEquatable();
    }
}
