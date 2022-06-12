﻿namespace SharpMeasures.Generators.Vectors.Pipelines.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Vectors.Refinement;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Vector, int Dimension, NamedType? Scalar, UnitInterface Unit, NamedType UnitQuantity,
    ReadOnlyEquatableCollection<UnitInstance> Units, ReadOnlyEquatableCollection<RefinedVectorConstantDefinition> Constants, DocumentationFile Documentation)
{
    public DataModel(DefinedType vector, int dimension, NamedType? scalar, UnitInterface unit, NamedType unitQuantity, IReadOnlyCollection<UnitInstance> units,
        IReadOnlyCollection<RefinedVectorConstantDefinition> constants, DocumentationFile documentation)
        : this(vector, dimension, scalar, unit, unitQuantity, units.AsReadOnlyEquatable(), constants.AsReadOnlyEquatable(), documentation) { }
}