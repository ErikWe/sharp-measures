namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, DocumentationFile Documentation);