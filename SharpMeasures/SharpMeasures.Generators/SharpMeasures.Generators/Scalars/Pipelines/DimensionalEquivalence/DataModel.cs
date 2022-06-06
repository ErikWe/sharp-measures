namespace SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Processing;

internal readonly record struct DataModel(DefinedType Scalar, ProcessedDimensionalEquivalence DimensionalEquivalences, DocumentationFile Documentation);
