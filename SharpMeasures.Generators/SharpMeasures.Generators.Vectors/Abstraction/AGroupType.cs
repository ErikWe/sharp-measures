namespace SharpMeasures.Generators.Vectors.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal record class AGroupType<TDefinition> : IVectorGroupType where TDefinition : IVectorGroup
{
    public DefinedType Type { get; }

    public TDefinition Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations { get; }
    public IReadOnlyList<ConvertibleVectorDefinition> Conversions { get; }

    public IReadOnlyList<IncludeUnitsDefinition> UnitInstanceInclusions { get; }
    public IReadOnlyList<ExcludeUnitsDefinition> UnitInstanceExclusions { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IVectorGroup IVectorGroupType.Definition => Definition;

    IReadOnlyList<IDerivedQuantity> IQuantityType.Derivations => Derivations;
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;

    IReadOnlyList<IUnitInstanceInclusionList> IQuantityType.UnitInstanceInclusions => UnitInstanceInclusions;
    IReadOnlyList<IUnitInstanceList> IQuantityType.UnitInstanceExclusions => UnitInstanceExclusions;

    protected AGroupType(DefinedType type, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;

        Definition = definition;

        Derivations = derivations.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        UnitInstanceInclusions = unitInstanceInclusions.AsReadOnlyEquatable();
        UnitInstanceExclusions = unitInstanceExclusions.AsReadOnlyEquatable();
    }
}
