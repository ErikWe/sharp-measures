namespace SharpMeasures.Generators.Scalars.Pipelines.Common;

using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars.Documentation;

internal readonly record struct DataModel(DefinedType Scalar, NamedType Unit, NamedType UnitQuantity, bool Biased, string? DefaultUnitName,
    string? DefaultUnitSymbol, IDocumentationStrategy Documentation);
