namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Scalar, NamedType Unit, NamedType UnitQuantity, ReadOnlyEquatableCollection<UnitInstance> Bases,
    ReadOnlyEquatableCollection<UnitInstance> Units, ReadOnlyEquatableCollection<RefinedScalarConstantDefinition> Constants, IDocumentationStrategy Documentation)
{
    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, IReadOnlyCollection<UnitInstance> bases, IReadOnlyCollection<UnitInstance> units,
        IReadOnlyCollection<RefinedScalarConstantDefinition> constants, IDocumentationStrategy documentation)
        : this(scalar, unit, unitQuantity, bases.AsReadOnlyEquatable(), units.AsReadOnlyEquatable(), constants.AsReadOnlyEquatable(), documentation) { }
}
