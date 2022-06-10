namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Vectors;

internal readonly record struct DataModel(DefinedType Scalar, ResizedVectorGroup Vectors, DocumentationFile Documentation);
