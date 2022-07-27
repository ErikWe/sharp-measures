﻿namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Generic;
using System.Linq;

internal abstract record class AUnresolvedScalarType<TDefinition> : IUnresolvedScalarType
    where TDefinition : IUnresolvedScalar
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TDefinition Definition { get; }
    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IUnresolvedQuantity IUnresolvedQuantityType.Definition => Definition;
    IUnresolvedScalar IUnresolvedScalarType.Definition => Definition;

    public IReadOnlyList<UnresolvedDerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<UnresolvedScalarConstantDefinition> Constants => constants;
    public IReadOnlyList<UnresolvedConvertibleScalarDefinition> Conversions => conversions;

    public IReadOnlyList<UnresolvedUnitListDefinition> BaseInclusions => baseInclusions;
    public IReadOnlyList<UnresolvedUnitListDefinition> BaseExclusions => baseExclusions;

    public IReadOnlyList<UnresolvedUnitListDefinition> UnitInclusions => unitInclusions;
    public IReadOnlyList<UnresolvedUnitListDefinition> UnitExclusions => unitExclusions;

    public IReadOnlyDictionary<string, IUnresolvedScalarConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IUnresolvedScalarConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<UnresolvedDerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<UnresolvedScalarConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<UnresolvedConvertibleScalarDefinition> conversions { get; }

    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> baseInclusions { get; }
    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> baseExclusions { get; }

    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> unitInclusions { get; }
    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> unitExclusions { get; }

    private ReadOnlyEquatableDictionary<string, IUnresolvedScalarConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IUnresolvedScalarConstant> constantsByMultiplesName { get; }

    IReadOnlyList<IUnresolvedDerivedQuantity> IUnresolvedQuantityType.Derivations => Derivations;
    IReadOnlyList<IUnresolvedScalarConstant> IUnresolvedScalarType.Constants => Constants;
    IReadOnlyList<IUnresolvedConvertibleQuantity> IUnresolvedQuantityType.Conversions => Conversions;
    IReadOnlyList<IUnresolvedConvertibleScalar> IUnresolvedScalarType.Conversions => Conversions;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedScalarType.BaseInclusions => BaseInclusions;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedScalarType.BaseExclusion => BaseExclusions;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedQuantityType.UnitInclusions => UnitInclusions;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedQuantityType.UnitExclusions => UnitExclusions;

    protected AUnresolvedScalarType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
        IReadOnlyList<UnresolvedScalarConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleScalarDefinition> conversions,
        IReadOnlyList<UnresolvedUnitListDefinition> baseInclusions, IReadOnlyList<UnresolvedUnitListDefinition> baseExclusions,
        IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.baseInclusions = baseInclusions.AsReadOnlyEquatable();
        this.baseExclusions = baseExclusions.AsReadOnlyEquatable();

        this.unitInclusions = unitInclusions.AsReadOnlyEquatable();
        this.unitExclusions = unitExclusions.AsReadOnlyEquatable();

        constantsByName = ConstructConstantsByNameDictionary();
        constantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IUnresolvedScalarConstant> ConstructConstantsByNameDictionary()
        => (Constants as IEnumerable<IUnresolvedScalarConstant>).ToDictionary(static (constant) => constant.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IUnresolvedScalarConstant> ConstructConstantsByMultiplesNameDictionary()
        => (Constants as IEnumerable<IUnresolvedScalarConstant>).Where(static (constant) => constant.Multiples is not null)
        .ToDictionary(static (constant) => constant.Multiples!).AsReadOnlyEquatable();
}