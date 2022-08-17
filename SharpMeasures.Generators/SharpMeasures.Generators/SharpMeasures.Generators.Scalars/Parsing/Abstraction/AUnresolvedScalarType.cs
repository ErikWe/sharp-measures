namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;

using System.Collections.Generic;
using System.Linq;

internal abstract record class AUnresolvedScalarType<TDefinition> : IRawScalarType
    where TDefinition : IRawScalar
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TDefinition Definition { get; }
    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IRawQuantity IRawQuantityType.Definition => Definition;
    IRawScalar IRawScalarType.Definition => Definition;

    public IReadOnlyList<RawDerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<UnresolvedScalarConstantDefinition> Constants => constants;
    public IReadOnlyList<UnresolvedConvertibleScalarDefinition> Conversions => conversions;

    public IReadOnlyList<RawUnitListDefinition> BaseInclusions => baseInclusions;
    public IReadOnlyList<RawUnitListDefinition> BaseExclusions => baseExclusions;

    public IReadOnlyList<RawUnitListDefinition> UnitInclusions => unitInclusions;
    public IReadOnlyList<RawUnitListDefinition> UnitExclusions => unitExclusions;

    public IReadOnlyDictionary<string, IRawScalarConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IRawScalarConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<RawDerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<UnresolvedScalarConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<UnresolvedConvertibleScalarDefinition> conversions { get; }

    private ReadOnlyEquatableList<RawUnitListDefinition> baseInclusions { get; }
    private ReadOnlyEquatableList<RawUnitListDefinition> baseExclusions { get; }

    private ReadOnlyEquatableList<RawUnitListDefinition> unitInclusions { get; }
    private ReadOnlyEquatableList<RawUnitListDefinition> unitExclusions { get; }

    private ReadOnlyEquatableDictionary<string, IRawScalarConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IRawScalarConstant> constantsByMultiplesName { get; }

    IReadOnlyList<IRawDerivedQuantity> IRawQuantityType.Derivations => Derivations;
    IReadOnlyList<IRawScalarConstant> IRawScalarType.Constants => Constants;
    IReadOnlyList<IRawConvertibleQuantity> IRawQuantityType.Conversions => Conversions;
    IReadOnlyList<IRawConvertibleScalar> IRawScalarType.Conversions => Conversions;
    IReadOnlyList<IRawUnitList> IRawScalarType.BaseInclusions => BaseInclusions;
    IReadOnlyList<IRawUnitList> IRawScalarType.BaseExclusion => BaseExclusions;
    IReadOnlyList<IRawUnitList> IRawQuantityType.UnitInclusions => UnitInclusions;
    IReadOnlyList<IRawUnitList> IRawQuantityType.UnitExclusions => UnitExclusions;

    protected AUnresolvedScalarType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<RawDerivedQuantityDefinition> derivations,
        IReadOnlyList<UnresolvedScalarConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleScalarDefinition> conversions,
        IReadOnlyList<RawUnitListDefinition> baseInclusions, IReadOnlyList<RawUnitListDefinition> baseExclusions,
        IReadOnlyList<RawUnitListDefinition> unitInclusions, IReadOnlyList<RawUnitListDefinition> unitExclusions)
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

    private ReadOnlyEquatableDictionary<string, IRawScalarConstant> ConstructConstantsByNameDictionary()
        => (Constants as IEnumerable<IRawScalarConstant>).ToDictionary(static (constant) => constant.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IRawScalarConstant> ConstructConstantsByMultiplesNameDictionary()
        => (Constants as IEnumerable<IRawScalarConstant>).Where(static (constant) => constant.Multiples is not null)
        .ToDictionary(static (constant) => constant.Multiples!).AsReadOnlyEquatable();
}
