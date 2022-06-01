namespace SharpMeasures.Generators.Scalars.Pipelines.Misc;

using SharpMeasures.Generators.Documentation;

internal readonly record struct DataModel(DefinedType Scalar, NamedType Unit, NamedType UnitQuantity, bool Biased, string? DefaultUnitName,
    string? DefaultUnitSymbol, DocumentationFile Documentation);