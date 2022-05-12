namespace SharpMeasures.Generators.Units.Pipeline.MiscPipeline;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, bool Biased, DocumentationFile Documentation);
