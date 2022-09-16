namespace SharpMeasures.Generators.Vectors.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal record class AGroupType<TDefinition> : IVectorGroupType
    where TDefinition : IVectorGroup
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TDefinition Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<ConvertibleVectorDefinition> Conversions => conversions;

    public IReadOnlyList<IncludeUnitsDefinition> UnitInstanceInclusions => unitInstanceInclusions;
    public IReadOnlyList<ExcludeUnitsDefinition> UnitInstanceExclusions => unitInstanceExclusions;

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<ConvertibleVectorDefinition> conversions { get; }

    private ReadOnlyEquatableList<IncludeUnitsDefinition> unitInstanceInclusions { get; }
    private ReadOnlyEquatableList<ExcludeUnitsDefinition> unitInstanceExclusions { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IVectorGroup IVectorGroupType.Definition => Definition;

    IReadOnlyList<IDerivedQuantity> IQuantityType.Derivations => Derivations;
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;

    IReadOnlyList<IUnitInstanceInclusionList> IQuantityType.UnitInstanceInclusions => UnitInstanceInclusions;
    IReadOnlyList<IUnitInstanceList> IQuantityType.UnitInstanceExclusions => UnitInstanceExclusions;

    protected AGroupType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.unitInstanceInclusions = unitInstanceInclusions.AsReadOnlyEquatable();
        this.unitInstanceExclusions = unitInstanceExclusions.AsReadOnlyEquatable();
    }
}
