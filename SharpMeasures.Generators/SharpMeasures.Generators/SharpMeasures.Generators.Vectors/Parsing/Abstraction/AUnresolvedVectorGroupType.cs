namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal abstract record class AUnresolvedVectorGroupType<TDefinition> : IUnresolvedVectorGroupType
    where TDefinition : IUnresolvedVectorGroup
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TDefinition Definition { get; }
    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IUnresolvedQuantity IUnresolvedQuantityType.Definition => Definition;
    IUnresolvedVectorGroup IUnresolvedVectorGroupType.Definition => Definition;

    public IReadOnlyList<UnresolvedDerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<UnresolvedConvertibleVectorDefinition> Conversions => conversions;

    public IReadOnlyList<UnresolvedUnitListDefinition> UnitInclusions => unitInclusions;
    public IReadOnlyList<UnresolvedUnitListDefinition> UnitExclusions => unitExclusions;

    private ReadOnlyEquatableList<UnresolvedDerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<UnresolvedConvertibleVectorDefinition> conversions { get; }

    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> unitInclusions { get; }
    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> unitExclusions { get; }

    IReadOnlyList<IUnresolvedDerivedQuantity> IUnresolvedQuantityType.Derivations => Derivations;
    IReadOnlyList<IUnresolvedConvertibleVector> IUnresolvedVectorGroupType.Conversions => Conversions;
    IReadOnlyList<IUnresolvedConvertibleQuantity> IUnresolvedQuantityType.Conversions => Conversions;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedQuantityType.UnitInclusions => UnitInclusions;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedQuantityType.UnitExclusions => UnitExclusions;

    protected AUnresolvedVectorGroupType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
        IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
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
