namespace SharpMeasures.Generators.Units.Pipelines.Common;

using SharpMeasures.Generators.Units.Documentation;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, bool Biased, IDocumentationStrategy Documentation);
