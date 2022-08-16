namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;
using System.Linq;

internal record class ScalarType : IScalarType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IScalar Definition { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IScalarConstant> Constants => constants;
    public IReadOnlyList<IConvertibleScalar> Conversions => conversions;

    public IReadOnlyList<IRawUnitInstance> IncludedBases => includedBases;
    public IReadOnlyList<IRawUnitInstance> IncludedUnits => includedUnits;

    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IScalarConstant> constants { get; }
    private ReadOnlyEquatableList<IConvertibleScalar> conversions { get; }

    private ReadOnlyEquatableList<IRawUnitInstance> includedBases { get; }
    private ReadOnlyEquatableList<IRawUnitInstance> includedUnits { get; }

    private ReadOnlyEquatableDictionary<string, IScalarConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IScalarConstant> constantsByMultiplesName { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;

    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;

    public ScalarType(DefinedType type, MinimalLocation typeLocation, IScalar definition, IReadOnlyList<IDerivedQuantity> derivations,
        IReadOnlyList<IScalarConstant> constants, IReadOnlyList<IConvertibleScalar> conversions, IReadOnlyList<IRawUnitInstance> includedBases,
        IReadOnlyList<IRawUnitInstance> includedUnits)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.includedBases = includedBases.AsReadOnlyEquatable();
        this.includedUnits = includedUnits.AsReadOnlyEquatable();

        constantsByName = ConstructConstantsByNameDictionary();
        constantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IScalarConstant> ConstructConstantsByNameDictionary()
        => Constants.ToDictionary(static (constant) => constant.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IScalarConstant> ConstructConstantsByMultiplesNameDictionary()
        => Constants.Where(static (constant) => constant.Multiples is not null) .ToDictionary(static (constant) => constant.Multiples!).AsReadOnlyEquatable();
}
