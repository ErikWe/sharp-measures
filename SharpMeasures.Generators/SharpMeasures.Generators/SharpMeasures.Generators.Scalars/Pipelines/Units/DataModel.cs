namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }

    public IReadOnlyList<IRawUnitInstance> IncludedBases => includedBases;
    public IReadOnlyList<IRawUnitInstance> IncluedUnits => includedUnits;

    public IReadOnlyList<IScalarConstant> Constants => constants;

    public IDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IRawUnitInstance> includedBases { get; }
    private ReadOnlyEquatableList<IRawUnitInstance> includedUnits { get; }

    private ReadOnlyEquatableList<IScalarConstant> constants { get; }

    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, IReadOnlyList<IRawUnitInstance> includedBases,
        IReadOnlyList<IRawUnitInstance> includedUnits, IReadOnlyList<IScalarConstant> constants, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;

        this.includedBases = includedBases.AsReadOnlyEquatable();
        this.includedUnits = includedUnits.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
