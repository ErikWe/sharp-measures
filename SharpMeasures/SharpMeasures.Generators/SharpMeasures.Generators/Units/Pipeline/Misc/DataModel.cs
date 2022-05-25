namespace SharpMeasures.Generators.Units.Pipeline.Misc;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, bool Biased, DocumentationFile Documentation);
