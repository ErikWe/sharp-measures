namespace SharpMeasures.Generators.Scalars.Pipelines.Vector;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Vectors;

internal readonly record struct DataModel(DefinedType Scalar, VectorCollection VectorCollection, DocumentationFile Documentation);