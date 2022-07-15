namespace SharpMeasures.Generators.Scalars.Parsing;

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
    public IReadOnlyList<UnresolvedConvertibleScalarDefinition> ConvertibleScalars => convertibleScalars;

    public IReadOnlyList<UnresolvedUnitListDefinition> BaseInclusions => includeBases;
    public IReadOnlyList<UnresolvedUnitListDefinition> BaseExclusions => excludeBases;

    public IReadOnlyList<UnresolvedUnitListDefinition> UnitInclusions => includeUnits;
    public IReadOnlyList<UnresolvedUnitListDefinition> UnitExclusions => excludeUnits;

    public IReadOnlyDictionary<string, IUnresolvedScalarConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IUnresolvedScalarConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    IReadOnlyDictionary<string, IUnresolvedQuantityConstant> IUnresolvedQuantityType.ConstantsByName
        => ConstantsByName.Transform(static (constant) => constant as IUnresolvedQuantityConstant);

    IReadOnlyDictionary<string, IUnresolvedQuantityConstant> IUnresolvedQuantityType.ConstantsByMultiplesName
        => ConstantsByMultiplesName.Transform(static (constant) => constant as IUnresolvedQuantityConstant);

    private ReadOnlyEquatableList<UnresolvedDerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<UnresolvedScalarConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<UnresolvedConvertibleScalarDefinition> convertibleScalars { get; }

    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> includeBases { get; }
    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> excludeBases { get; }

    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> includeUnits { get; }
    private ReadOnlyEquatableList<UnresolvedUnitListDefinition> excludeUnits { get; }

    private ReadOnlyEquatableDictionary<string, IUnresolvedScalarConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IUnresolvedScalarConstant> constantsByMultiplesName { get; }

    IReadOnlyList<IUnresolvedDerivedQuantity> IUnresolvedQuantityType.Derivations => Derivations;
    IReadOnlyList<IUnresolvedQuantityConstant> IUnresolvedQuantityType.Constants => Constants;
    IReadOnlyList<IUnresolvedScalarConstant> IUnresolvedScalarType.Constants => Constants;
    IReadOnlyList<IUnresolvedConvertibleQuantity> IUnresolvedQuantityType.ConvertibleQuantities => ConvertibleScalars;
    IReadOnlyList<IUnresolvedConvertibleScalar> IUnresolvedScalarType.ConvertibleScalars => ConvertibleScalars;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedScalarType.BaseInclusions => BaseInclusions;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedScalarType.BaseExclusion => BaseExclusions;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedQuantityType.UnitInclusions => UnitInclusions;
    IReadOnlyList<IUnresolvedUnitList> IUnresolvedQuantityType.UnitExclusions => UnitExclusions;

    protected AUnresolvedScalarType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
        IReadOnlyList<UnresolvedScalarConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleScalarDefinition> convertibleScalars,
        IReadOnlyList<UnresolvedUnitListDefinition> baseInclusions, IReadOnlyList<UnresolvedUnitListDefinition> baseExclusions,
        IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.convertibleScalars = convertibleScalars.AsReadOnlyEquatable();

        this.includeBases = baseInclusions.AsReadOnlyEquatable();
        this.excludeBases = baseExclusions.AsReadOnlyEquatable();

        this.includeUnits = unitInclusions.AsReadOnlyEquatable();
        this.excludeUnits = unitExclusions.AsReadOnlyEquatable();

        this.constantsByName = ConstructConstantsByNameDictionary();
        this.constantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IUnresolvedScalarConstant> ConstructConstantsByNameDictionary()
        => (Constants as IEnumerable<IUnresolvedScalarConstant>).ToDictionary(static (constant) => constant.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IUnresolvedScalarConstant> ConstructConstantsByMultiplesNameDictionary()
        => (Constants as IEnumerable<IUnresolvedScalarConstant>).Where(static (constant) => constant.Multiples is not null)
        .ToDictionary(static (constant) => constant.Multiples!).AsReadOnlyEquatable();
}
