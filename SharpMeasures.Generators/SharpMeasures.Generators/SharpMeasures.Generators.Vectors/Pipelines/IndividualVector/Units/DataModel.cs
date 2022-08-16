namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }

    public int Dimension { get; }

    public NamedType? Scalar { get; }
    
    public IRawUnitType Unit { get; }
    public NamedType UnitQuantity { get; }

    public IReadOnlyList<IRawUnitInstance> Units => units;
    public IReadOnlyList<IVectorConstant> Constants => constants;

    public IIndividualVectorDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IRawUnitInstance> units { get; }
    private ReadOnlyEquatableList<IVectorConstant> constants { get; }

    public DataModel(DefinedType vector, int dimension, NamedType? scalar, IRawUnitType unit, NamedType unitQuantity, IReadOnlyList<IRawUnitInstance> units,
        IReadOnlyList<IVectorConstant> constants, IIndividualVectorDocumentationStrategy documentation)
    {
        Vector = vector;

        Dimension = dimension;

        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;

        this.units = units.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
