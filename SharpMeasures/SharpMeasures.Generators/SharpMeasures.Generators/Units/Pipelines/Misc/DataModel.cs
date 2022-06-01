namespace SharpMeasures.Generators.Units.Pipelines.Misc;

using SharpMeasures.Generators.Documentation;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, bool Biased, DocumentationFile Documentation);
