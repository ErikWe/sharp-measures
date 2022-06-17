namespace SharpMeasures.Generators.Scalars.Pipelines.Maths;

using SharpMeasures.Generators.Scalars.Documentation;

internal readonly record struct DataModel(DefinedType Scalar, NamedType Unit, bool ImplementSum, bool ImplementDifference, NamedType Difference, NamedType? Reciprocal,
    NamedType? Square, NamedType? Cube, NamedType? SquareRoot, NamedType? CubeRoot, IDocumentationStrategy Documentation);
