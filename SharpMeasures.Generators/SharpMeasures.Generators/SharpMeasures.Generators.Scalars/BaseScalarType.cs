namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

internal record class BaseScalarType : IScalarType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public SharpMeasuresScalarDefinition ScalarDefinition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<ScalarConstantDefinition> Constants => constants;
    public IReadOnlyList<ConvertibleQuantityDefinition> ConvertibleQuantities => convertibleQuantities;

    public IReadOnlyList<IUnitInstance> IncludedBases => includedBases;
    public IReadOnlyList<IUnitInstance> IncludedUnits => includedUnits;

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<ScalarConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<ConvertibleQuantityDefinition> convertibleQuantities { get; }

    private ReadOnlyEquatableList<IUnitInstance> includedBases { get; }
    private ReadOnlyEquatableList<IUnitInstance> includedUnits { get; }

    IScalar IScalarType.ScalarDefinition => ScalarDefinition;

    IReadOnlyList<IDerivedQuantity> IScalarType.Derivations => Derivations;
    IReadOnlyList<IScalarConstant> IScalarType.Constants => Constants;
    IReadOnlyList<IConvertibleQuantity> IScalarType.ConvertibleQuantities => ConvertibleQuantities;

    public BaseScalarType(DefinedType type, MinimalLocation typeLocation, SharpMeasuresScalarDefinition scalarDefinition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleQuantityDefinition> convertibleQuantities, IReadOnlyList<IUnitInstance> includedBases,
        IReadOnlyList<IUnitInstance> includedUnits)
    {
        Type = type;
        TypeLocation = typeLocation;
        ScalarDefinition = scalarDefinition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.convertibleQuantities = convertibleQuantities.AsReadOnlyEquatable();

        this.includedBases = includedBases.AsReadOnlyEquatable();
        this.includedUnits = includedUnits.AsReadOnlyEquatable();
    }
}
