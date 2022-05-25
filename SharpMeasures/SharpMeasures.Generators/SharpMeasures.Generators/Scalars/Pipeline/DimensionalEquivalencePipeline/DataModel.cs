namespace SharpMeasures.Generators.Scalars.Pipeline.DimensionalEquivalencePipeline;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

internal readonly record struct DataModel(DefinedType Scalar, CacheableDimensionalEquivalenceDefinition DimensionalEquivalence, DocumentationFile Documentation);