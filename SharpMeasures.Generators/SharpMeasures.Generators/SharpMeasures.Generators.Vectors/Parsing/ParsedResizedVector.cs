namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Abstrations;
using SharpMeasures.Generators.Quantities.Parsing.Abstractions;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal class ParsedResizedVector : IInclusionExclusion<UnresolvedIncludeUnitsDefinition, UnresolvedExcludeUnitsDefinition>
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public ResizedSharpMeasuresVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<UnresolvedIncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<UnresolvedExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<VectorConstantDefinition> VectorConstants { get; }
    public EquatableEnumerable<UnresolvedConvertibleQuantityDefinition> DimensionalEquivalences { get; }

    IEnumerable<UnresolvedIncludeUnitsDefinition> IInclusionExclusion<UnresolvedIncludeUnitsDefinition, UnresolvedExcludeUnitsDefinition>.IncludeUnits => IncludeUnits;
    IEnumerable<UnresolvedExcludeUnitsDefinition> IInclusionExclusion<UnresolvedIncludeUnitsDefinition, UnresolvedExcludeUnitsDefinition>.ExcludeUnits => ExcludeUnits;

    public ParsedResizedVector(DefinedType vectorType, MinimalLocation vectorLocation, ResizedSharpMeasuresVectorDefinition vectorDefinition,
        IEnumerable<UnresolvedIncludeUnitsDefinition> includeUnits, IEnumerable<UnresolvedExcludeUnitsDefinition> excludeUnits, IEnumerable<VectorConstantDefinition> vectorConstants,
        IEnumerable<UnresolvedConvertibleQuantityDefinition> dimensionalEquivalences)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;

        IncludeUnits = includeUnits.AsEquatable();
        ExcludeUnits = excludeUnits.AsEquatable();

        VectorConstants = vectorConstants.AsEquatable();
        DimensionalEquivalences = dimensionalEquivalences.AsEquatable();
    }
}
