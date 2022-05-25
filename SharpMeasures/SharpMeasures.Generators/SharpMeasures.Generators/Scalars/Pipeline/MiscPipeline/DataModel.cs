namespace SharpMeasures.Generators.Scalars.Pipeline.MiscPipeline;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

internal readonly record struct DataModel(DefinedType Scalar, NamedType Unit, NamedType UnitQuantity, bool Biased, DocumentationFile Documentation);