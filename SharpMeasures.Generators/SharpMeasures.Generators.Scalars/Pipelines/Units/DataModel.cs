namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }

    public IReadOnlyList<IUnitInstance> IncludedBases => includedBases;
    public IReadOnlyList<IUnitInstance> IncluedUnits => includedUnits;

    public IReadOnlyList<IScalarConstant> Constants => constants;

    public IDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IUnitInstance> includedBases { get; }
    private ReadOnlyEquatableList<IUnitInstance> includedUnits { get; }

    private ReadOnlyEquatableList<IScalarConstant> constants { get; }

    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, IReadOnlyList<IUnitInstance> includedBases, IReadOnlyList<IUnitInstance> includedUnits,
        IReadOnlyList<IScalarConstant> constants, IDocumentationStrategy documentation)
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
