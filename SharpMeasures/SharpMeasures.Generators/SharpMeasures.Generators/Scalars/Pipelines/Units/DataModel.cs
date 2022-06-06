namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Processing;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Scalar, NamedType Unit, NamedType UnitQuantity, EquatableCollection<UnitInstance> Bases,
    EquatableCollection<UnitInstance> Units, EquatableCollection<ProcessedScalarConstant> Constants, DocumentationFile Documentation)
{
    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, IReadOnlyCollection<UnitInstance> bases, IReadOnlyCollection<UnitInstance> units,
        IReadOnlyCollection<ProcessedScalarConstant> constants, DocumentationFile documentation)
        : this(scalar, unit, unitQuantity, new(bases), new(units), new(constants), documentation) { }
}
