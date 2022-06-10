namespace SharpMeasures.Generators.Units.Pipelines.Common;

using SharpMeasures.Generators.Documentation;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, bool Biased, DocumentationFile Documentation);
