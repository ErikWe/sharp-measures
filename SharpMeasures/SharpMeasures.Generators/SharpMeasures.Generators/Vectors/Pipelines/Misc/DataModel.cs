namespace SharpMeasures.Generators.Vectors.Pipelines.Misc;

using SharpMeasures.Generators.Documentation;

internal readonly record struct DataModel(DefinedType Vector, int Dimension, NamedType? Scalar, NamedType Unit, NamedType UnitQuantity, string? DefaultUnitName,
    string? DefaultUnitSymbol, DocumentationFile Documentation);
