namespace SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Refinement;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Scalar, EquatableEnumerable<RefinedDimensionalEquivalenceDefinition> DimensionalEquivalences,
    DocumentationFile Documentation)
{
    public DataModel(DefinedType scalar, IEnumerable<RefinedDimensionalEquivalenceDefinition> dimensionalEquivalences, DocumentationFile documentation)
        : this(scalar, new(dimensionalEquivalences), documentation) { }
}
