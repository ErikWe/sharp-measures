namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Vectors;

internal readonly record struct DataModel(DefinedType Scalar, ResizedGroup Vectors, IDocumentationStrategy Documentation);
