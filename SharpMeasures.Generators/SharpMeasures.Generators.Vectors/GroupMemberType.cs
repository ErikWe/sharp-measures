namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;

internal record class GroupMemberType : IVectorGroupMemberType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public SharpMeasuresVectorGroupMemberDefinition Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<VectorConstantDefinition> Constants => constants;
    public IReadOnlyList<ConvertibleVectorDefinition> Conversions => conversions;

    public IReadOnlyList<IncludeUnitsDefinition> UnitInclusions => unitInclusions;
    public IReadOnlyList<ExcludeUnitsDefinition> UnitExclusions => unitExclusions;

    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByName => constantsByName;
    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByMultiplesName => constantsByMultiplesName;

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<VectorConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<ConvertibleVectorDefinition> conversions { get; }

    private ReadOnlyEquatableList<IncludeUnitsDefinition> unitInclusions { get; }
    private ReadOnlyEquatableList<ExcludeUnitsDefinition> unitExclusions { get; }

    private ReadOnlyEquatableDictionary<string, IVectorConstant> constantsByName { get; }
    private ReadOnlyEquatableDictionary<string, IVectorConstant> constantsByMultiplesName { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IVectorGroupMember IVectorGroupMemberType.Definition => Definition;

    IReadOnlyList<IDerivedQuantity> IQuantityType.Derivations => Derivations;
    IReadOnlyList<IVectorConstant> IVectorGroupMemberType.Constants => Constants;
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;
    IReadOnlyList<IConvertibleVector> IVectorGroupMemberType.Conversions => Conversions;

    IReadOnlyList<IUnitList> IQuantityType.UnitInclusions => UnitInclusions;
    IReadOnlyList<IUnitList> IQuantityType.UnitExclusions => UnitExclusions;

    public GroupMemberType(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorGroupMemberDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInclusions,
        IReadOnlyList<ExcludeUnitsDefinition> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.unitInclusions = unitInclusions.AsReadOnlyEquatable();
        this.unitExclusions = unitExclusions.AsReadOnlyEquatable();

        constantsByName = ConstructConstantsByNameDictionary();
        constantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IVectorConstant> ConstructConstantsByNameDictionary()
        => Constants.ToDictionary(static (constant) => constant.Name, static (constant) => constant as IVectorConstant).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IVectorConstant> ConstructConstantsByMultiplesNameDictionary()
        => Constants.Where(static (constant) => constant.Multiples is not null).ToDictionary(static (constant) => constant.Multiples!, static (constant) => constant as IVectorConstant).AsReadOnlyEquatable();
}
