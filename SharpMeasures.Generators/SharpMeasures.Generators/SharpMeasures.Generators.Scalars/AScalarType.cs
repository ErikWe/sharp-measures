namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;
using System.Linq;

internal abstract record class AScalarType<TScalar> : IScalarType
    where TScalar : IScalar
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TScalar Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<ScalarConstantDefinition> Constants => constants;
    public IReadOnlyList<ConvertibleScalarDefinition> ConvertibleScalars => convertibleScalars;

    public IReadOnlyList<UnitListDefinition> BaseInclusions => includeBases;
    public IReadOnlyList<UnitListDefinition> BaseExclusions => excludeBases;

    public IReadOnlyList<UnitListDefinition> UnitInclusions => includeUnits;
    public IReadOnlyList<UnitListDefinition> UnitExclusions => excludeUnits;

    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    IReadOnlyDictionary<string, IQuantityConstant> IQuantityType.ConstantsByName
        => ConstantsByName.Transform(static (constant) => constant as IQuantityConstant);

    IReadOnlyDictionary<string, IQuantityConstant> IQuantityType.ConstantsByMultiplesName
        => ConstantsByMultiplesName.Transform(static (constant) => constant as IQuantityConstant);

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<ScalarConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<ConvertibleScalarDefinition> convertibleScalars { get; }

    private ReadOnlyEquatableList<UnitListDefinition> includeBases { get; }
    private ReadOnlyEquatableList<UnitListDefinition> excludeBases { get; }

    private ReadOnlyEquatableList<UnitListDefinition> includeUnits { get; }
    private ReadOnlyEquatableList<UnitListDefinition> excludeUnits { get; }

    private ReadOnlyEquatableDictionary<string, IScalarConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IScalarConstant> constantsByMultiplesName { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IScalar IScalarType.Definition => Definition;

    IReadOnlyList<IDerivedQuantity> IQuantityType.Derivations => Derivations;
    IReadOnlyList<IQuantityConstant> IQuantityType.Constants => Constants;
    IReadOnlyList<IScalarConstant> IScalarType.Constants => Constants;
    IReadOnlyList<IConvertibleQuantity> IQuantityType.ConvertibleQuantities => ConvertibleScalars;
    IReadOnlyList<IConvertibleScalar> IScalarType.ConvertibleScalars => ConvertibleScalars;

    IReadOnlyList<IUnitList> IScalarType.BaseInclusions => BaseInclusions;
    IReadOnlyList<IUnitList> IScalarType.BaseExclusions => BaseExclusions;

    IReadOnlyList<IUnitList> IQuantityType.UnitInclusions => UnitInclusions;
    IReadOnlyList<IUnitList> IQuantityType.UnitExclusions => UnitExclusions;

    protected AScalarType(DefinedType type, MinimalLocation typeLocation, TScalar definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> convertibleScalars,
        IReadOnlyList<UnitListDefinition> baseInclusions, IReadOnlyList<UnitListDefinition> baseExclusions,
        IReadOnlyList<UnitListDefinition> unitInclusions, IReadOnlyList<UnitListDefinition> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.convertibleScalars = convertibleScalars.AsReadOnlyEquatable();

        includeBases = baseInclusions.AsReadOnlyEquatable();
        excludeBases = baseExclusions.AsReadOnlyEquatable();

        includeUnits = unitInclusions.AsReadOnlyEquatable();
        excludeUnits = unitExclusions.AsReadOnlyEquatable();

        constantsByName = ConstructConstantsByNameDictionary();
        constantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IScalarConstant> ConstructConstantsByNameDictionary()
        => (Constants as IEnumerable<IScalarConstant>).ToDictionary(static (constant) => constant.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IScalarConstant> ConstructConstantsByMultiplesNameDictionary()
        => (Constants as IEnumerable<IScalarConstant>).Where(static (constant) => constant.Multiples is not null)
        .ToDictionary(static (constant) => constant.Multiples!).AsReadOnlyEquatable();
}
