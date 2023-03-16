namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }
    public int Dimension { get; }

    public NamedType? Scalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }

    public IReadOnlyList<IUnitInstance> IncludedUnits { get; }
    public IReadOnlyList<IVectorConstant> Constants { get; }

    public VectorSourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType vector, int dimension, NamedType? scalar, NamedType unit, NamedType unitQuantity, IReadOnlyList<IUnitInstance> includedUnits, IReadOnlyList<IVectorConstant> constants, VectorSourceBuildingContext sourceBuildingContext)
    {
        Vector = vector;
        Dimension = dimension;

        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;

        IncludedUnits = includedUnits.AsReadOnlyEquatable();
        Constants = constants.AsReadOnlyEquatable();

        SourceBuildingContext = sourceBuildingContext;
    }
}
