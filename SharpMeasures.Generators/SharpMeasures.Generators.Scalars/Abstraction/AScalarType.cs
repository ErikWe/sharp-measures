namespace SharpMeasures.Generators.Scalars.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;
using System.Linq;

internal record class AScalarType<TDefinition> : IScalarType where TDefinition : IScalar
{
    public DefinedType Type { get; }

    public TDefinition Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations { get; }
    public IReadOnlyList<ScalarConstantDefinition> Constants { get; }
    public IReadOnlyList<ConvertibleScalarDefinition> Conversions { get; }

    public IReadOnlyList<IncludeUnitBasesDefinition> UnitBaseInstanceInclusions { get; }
    public IReadOnlyList<ExcludeUnitBasesDefinition> UnitBaseInstanceExclusions { get; }

    public IReadOnlyList<IncludeUnitsDefinition> UnitInstanceInclusions { get; }
    public IReadOnlyList<ExcludeUnitsDefinition> UnitInstanceExclusions { get; }

    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IScalarConstant> ConstantsByMultiplesName { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IScalar IScalarType.Definition => Definition;

    IReadOnlyList<IDerivedQuantity> IQuantityType.Derivations => Derivations;
    IReadOnlyList<IScalarConstant> IScalarType.Constants => Constants; 
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;

    IReadOnlyList<IUnitInstanceInclusionList> IScalarType.UnitBaseInstanceInclusions => UnitBaseInstanceInclusions;
    IReadOnlyList<IUnitInstanceList> IScalarType.UnitBaseInstanceExclusions => UnitBaseInstanceExclusions;

    IReadOnlyList<IUnitInstanceInclusionList> IQuantityType.UnitInstanceInclusions => UnitInstanceInclusions;
    IReadOnlyList<IUnitInstanceList> IQuantityType.UnitInstanceExclusions => UnitInstanceExclusions;

    protected AScalarType(DefinedType type, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions,
        IReadOnlyList<IncludeUnitBasesDefinition> unitBaseInstanceInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> unitBaseInstanceExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;

        Definition = definition;

        Derivations = derivations.AsReadOnlyEquatable();
        Constants = constants.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        UnitBaseInstanceInclusions = unitBaseInstanceInclusions.AsReadOnlyEquatable();
        UnitBaseInstanceExclusions = unitBaseInstanceExclusions.AsReadOnlyEquatable();

        UnitInstanceInclusions = unitInstanceInclusions.AsReadOnlyEquatable();
        UnitInstanceExclusions = unitInstanceExclusions.AsReadOnlyEquatable();

        ConstantsByName = ConstructConstantsByNameDictionary();
        ConstantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IScalarConstant> ConstructConstantsByNameDictionary() => Constants.ToDictionary(static (constant) => constant.Name, static (constant) => constant as IScalarConstant).AsReadOnlyEquatable();
    private ReadOnlyEquatableDictionary<string, IScalarConstant> ConstructConstantsByMultiplesNameDictionary() => Constants.Where(static (constant) => constant.Multiples is not null).ToDictionary(static (constant) => constant.Multiples!, static (constant) => constant as IScalarConstant).AsReadOnlyEquatable();
}
