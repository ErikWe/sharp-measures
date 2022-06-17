namespace SharpMeasures.Generators.Vectors.Pipelines.Maths;

using SharpMeasures.Generators.Vectors.Documentation;

internal readonly record struct DataModel(DefinedType Vector, int Dimension, NamedType? Scalar, NamedType? SquaredScalar, NamedType Unit,
    NamedType UnitQuantity, IDocumentationStrategy Documentation);
