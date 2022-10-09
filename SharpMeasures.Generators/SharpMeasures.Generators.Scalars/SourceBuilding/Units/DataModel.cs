namespace SharpMeasures.Generators.Scalars.SourceBuilding.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }

    public IReadOnlyList<IUnitInstance> IncludedUnitBases { get; }
    public IReadOnlyList<IUnitInstance> IncludedUnits { get; }

    public IReadOnlyList<IScalarConstant> Constants { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, IReadOnlyList<IUnitInstance> includedBases, IReadOnlyList<IUnitInstance> includedUnits, IReadOnlyList<IScalarConstant> constants, SourceBuildingContext sourceBuildingContext)
    {
        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;

        IncludedUnitBases = includedBases.AsReadOnlyEquatable();
        IncludedUnits = includedUnits.AsReadOnlyEquatable();

        Constants = constants.AsReadOnlyEquatable();

        SourceBuildingContext = sourceBuildingContext;
    }
}
