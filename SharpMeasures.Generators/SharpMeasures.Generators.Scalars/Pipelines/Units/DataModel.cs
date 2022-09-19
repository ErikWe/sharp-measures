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

    public IReadOnlyList<IUnitInstance> IncludedUnitBases { get; }
    public IReadOnlyList<IUnitInstance> IncludedUnits { get; }

    public IReadOnlyList<IScalarConstant> Constants { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, IReadOnlyList<IUnitInstance> includedBases, IReadOnlyList<IUnitInstance> includedUnits, IReadOnlyList<IScalarConstant> constants, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;

        IncludedUnitBases = includedBases.AsReadOnlyEquatable();
        IncludedUnits = includedUnits.AsReadOnlyEquatable();

        Constants = constants.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
