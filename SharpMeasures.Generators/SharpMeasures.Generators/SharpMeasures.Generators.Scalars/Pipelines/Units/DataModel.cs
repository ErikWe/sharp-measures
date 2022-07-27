namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }

    public IReadOnlyList<IUnresolvedUnitInstance> IncludedBases => includedBases;
    public IReadOnlyList<IUnresolvedUnitInstance> IncluedUnits => includedUnits;

    public IReadOnlyList<IScalarConstant> Constants => constants;

    public IDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableList<IUnresolvedUnitInstance> includedBases { get; }
    private ReadOnlyEquatableList<IUnresolvedUnitInstance> includedUnits { get; }

    private ReadOnlyEquatableList<IScalarConstant> constants { get; }

    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, IReadOnlyList<IUnresolvedUnitInstance> includedBases,
        IReadOnlyList<IUnresolvedUnitInstance> includedUnits, IReadOnlyList<IScalarConstant> constants, IDocumentationStrategy documentation)
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
