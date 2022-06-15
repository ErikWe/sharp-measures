namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using SharpMeasures.Generators.Scalars.Documentation;

internal readonly record struct DataModel(DefinedType Scalar, NamedType Unit, NamedType? Reciprocal, NamedType? Square, NamedType? Cube,
    NamedType? SquareRoot, NamedType? CubeRoot, IDocumentationStrategy Documentation);
