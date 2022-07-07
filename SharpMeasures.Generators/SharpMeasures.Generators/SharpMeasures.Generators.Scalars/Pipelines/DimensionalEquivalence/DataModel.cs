namespace SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Scalars.Refinement.DimensionalEquivalence;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Scalar, EquatableEnumerable<RefinedConvertibleQuantityDefinition> DimensionalEquivalences,
    IDocumentationStrategy Documentation)
{
    public DataModel(DefinedType scalar, IEnumerable<RefinedConvertibleQuantityDefinition> dimensionalEquivalences, IDocumentationStrategy documentation)
        : this(scalar, dimensionalEquivalences.AsEquatable(), documentation) { }
}
