namespace SharpMeasures.Generators.Scalars.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;
using System.Linq;

internal record class AScalarType<TDefinition> : IScalarType
    where TDefinition : IScalar
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TDefinition Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<ScalarConstantDefinition> Constants => constants;
    public IReadOnlyList<ConvertibleScalarDefinition> Conversions => conversions;

    public IReadOnlyList<IncludeBasesDefinition> BaseInclusions => baseInclusions;
    public IReadOnlyList<ExcludeBasesDefinition> BaseExclusions => baseExclusions;

    public IReadOnlyList<IncludeUnitsDefinition> UnitInclusions => unitInclusions;
    public IReadOnlyList<ExcludeUnitsDefinition> UnitExclusions => unitExclusions;

    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<ScalarConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<ConvertibleScalarDefinition> conversions { get; }

    private ReadOnlyEquatableList<IncludeBasesDefinition> baseInclusions { get; }
    private ReadOnlyEquatableList<ExcludeBasesDefinition> baseExclusions { get; }

    private ReadOnlyEquatableList<IncludeUnitsDefinition> unitInclusions { get; }
    private ReadOnlyEquatableList<ExcludeUnitsDefinition> unitExclusions { get; }

    private ReadOnlyEquatableDictionary<string, IScalarConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IScalarConstant> constantsByMultiplesName { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IScalar IScalarType.Definition => Definition;

    IReadOnlyList<IDerivedQuantity> IQuantityType.Derivations => Derivations;
    IReadOnlyList<IScalarConstant> IScalarType.Constants => Constants; 
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;
    IReadOnlyList<IConvertibleScalar> IScalarType.Conversions => Conversions;

    IReadOnlyList<IUnitList> IScalarType.BaseInclusions => BaseInclusions;
    IReadOnlyList<IUnitList> IScalarType.BaseExclusions => BaseExclusions;

    IReadOnlyList<IUnitList> IQuantityType.UnitInclusions => UnitInclusions;
    IReadOnlyList<IUnitList> IQuantityType.UnitExclusions => UnitExclusions;

    protected AScalarType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants,
        IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeBasesDefinition> baseInclusions, IReadOnlyList<ExcludeBasesDefinition> baseExclusions,
        IReadOnlyList<IncludeUnitsDefinition> unitInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitExclusions)
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

    private ReadOnlyEquatableDictionary<string, IScalarConstant> ConstructConstantsByNameDictionary()
        => Constants.ToDictionary(static (constant) => constant.Name, static (constant) => constant as IScalarConstant).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IScalarConstant> ConstructConstantsByMultiplesNameDictionary()
        => Constants.Where(static (constant) => constant.Multiples is not null).ToDictionary(static (constant) => constant.Multiples!, static (constant) => constant as IScalarConstant).AsReadOnlyEquatable();
}
