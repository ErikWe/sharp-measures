namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Refinement;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Scalar, NamedType Unit, NamedType UnitQuantity, ReadOnlyEquatableCollection<UnitInstance> Bases,
    ReadOnlyEquatableCollection<UnitInstance> Units, ReadOnlyEquatableCollection<RefinedScalarConstantDefinition> Constants, DocumentationFile Documentation)
{
    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, IReadOnlyCollection<UnitInstance> bases, IReadOnlyCollection<UnitInstance> units,
        IReadOnlyCollection<RefinedScalarConstantDefinition> constants, DocumentationFile documentation)
        : this(scalar, unit, unitQuantity, bases.AsReadOnlyEquatable(), units.AsReadOnlyEquatable(), constants.AsReadOnlyEquatable(), documentation) { }
}
