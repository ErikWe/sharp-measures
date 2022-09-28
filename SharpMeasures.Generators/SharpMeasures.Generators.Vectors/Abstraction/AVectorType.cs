namespace SharpMeasures.Generators.Vectors.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using System.Linq;

internal record class AVectorType<TDefinition> : IVectorType where TDefinition : IVector
{
    public DefinedType Type { get; }

    public TDefinition Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations { get; }
    public IReadOnlyList<ProcessedQuantityDefinition> Processes { get; }
    public IReadOnlyList<VectorConstantDefinition> Constants { get; }
    public IReadOnlyList<ConvertibleVectorDefinition> Conversions { get; }

    public IReadOnlyList<IncludeUnitsDefinition> UnitInstanceInclusions { get; }
    public IReadOnlyList<ExcludeUnitsDefinition> UnitInstanceExclusions { get; }

    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByName { get; }
    public IReadOnlyDictionary<string, IVectorConstant> ConstantsByMultiplesName { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IQuantity IQuantityType.Definition => Definition;
    IVector IVectorType.Definition => Definition;

    IReadOnlyList<IDerivedQuantity> IQuantityType.Derivations => Derivations;
    IReadOnlyList<IProcessedQuantity> IVectorType.Processes => Processes;
    IReadOnlyList<IVectorConstant> IVectorType.Constants => Constants;
    IReadOnlyList<IConvertibleQuantity> IQuantityType.Conversions => Conversions;

    IReadOnlyList<IUnitInstanceInclusionList> IQuantityType.UnitInstanceInclusions => UnitInstanceInclusions;
    IReadOnlyList<IUnitInstanceList> IQuantityType.UnitInstanceExclusions => UnitInstanceExclusions;

    protected AVectorType(DefinedType type, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ProcessedQuantityDefinition> processes, IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;

        Definition = definition;

        Derivations = derivations.AsReadOnlyEquatable();
        Processes = processes.AsReadOnlyEquatable();
        Constants = constants.AsReadOnlyEquatable();
        Conversions = conversions.AsReadOnlyEquatable();

        UnitInstanceInclusions = unitInstanceInclusions.AsReadOnlyEquatable();
        UnitInstanceExclusions = unitInstanceExclusions.AsReadOnlyEquatable();

        ConstantsByName = ConstructConstantsByNameDictionary();
        ConstantsByMultiplesName = ConstructConstantsByMultiplesNameDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IVectorConstant> ConstructConstantsByNameDictionary() => Constants.ToDictionary(static (constant) => constant.Name, static (constant) => constant as IVectorConstant).AsReadOnlyEquatable();
    private ReadOnlyEquatableDictionary<string, IVectorConstant> ConstructConstantsByMultiplesNameDictionary() => Constants.Where(static (constant) => constant.Multiples is not null).ToDictionary(static (constant) => constant.Multiples!, static (constant) => constant as IVectorConstant).AsReadOnlyEquatable();
}
