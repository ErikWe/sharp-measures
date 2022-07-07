namespace SharpMeasures.Generators.Vectors.Pipelines.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Documentation;
using SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Vector, int Dimension, NamedType? Scalar, IUnitType Unit, NamedType UnitQuantity,
    ReadOnlyEquatableCollection<UnitInstance> Units, ReadOnlyEquatableCollection<RefinedVectorConstantDefinition> Constants, IDocumentationStrategy Documentation)
{
    public DataModel(DefinedType vector, int dimension, NamedType? scalar, IUnitType unit, NamedType unitQuantity, IReadOnlyCollection<UnitInstance> units,
        IReadOnlyCollection<RefinedVectorConstantDefinition> constants, IDocumentationStrategy documentation)
        : this(vector, dimension, scalar, unit, unitQuantity, units.AsReadOnlyEquatable(), constants.AsReadOnlyEquatable(), documentation) { }
}
