﻿namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal record class VectorGroupType : IVectorGroupType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IVectorGroup Definition { get; }

    public IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> MembersByDimension => registeredMembersByDimension;

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IConvertibleVector> Conversions => conversions;

    public IReadOnlyList<IUnresolvedUnitInstance> IncludedUnits => includedUnits;

    private ReadOnlyEquatableDictionary<int, IUnresolvedVectorGroupMemberType> registeredMembersByDimension { get; }

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IConvertibleVector> conversions { get; }

    private ReadOnlyEquatableList<IUnresolvedUnitInstance> includedUnits { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IVectorGroup IVectorGroupType.Definition => Definition;

    IReadOnlyList<IDerivedQuantity> IQuantityType.Derivations => Derivations;
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;
    IReadOnlyList<IConvertibleVector> IVectorGroupType.Conversions => Conversions;

    public VectorGroupType(DefinedType type, MinimalLocation typeLocation, IVectorGroup definition,
        IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> registeredMembersByDimension, IReadOnlyList<IDerivedQuantity> derivations,
        IReadOnlyList<IConvertibleVector> conversions, IReadOnlyList<IUnresolvedUnitInstance> includedUnits)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.registeredMembersByDimension = registeredMembersByDimension.AsReadOnlyEquatable();

        this.derivations = derivations.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.includedUnits = includedUnits.AsReadOnlyEquatable();
    }
}
