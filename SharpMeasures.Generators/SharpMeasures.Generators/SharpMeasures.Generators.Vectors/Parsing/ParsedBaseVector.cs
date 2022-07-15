namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Abstrations;
using SharpMeasures.Generators.Quantities.Parsing.Abstractions;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class ParsedBaseVector : IInclusionExclusion<UnresolvedIncludeUnitsDefinition, UnresolvedExcludeUnitsDefinition>
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public SharpMeasuresVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<UnresolvedIncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<UnresolvedExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<VectorConstantDefinition> VectorConstants { get; }
    public EquatableEnumerable<UnresolvedConvertibleQuantityDefinition> DimensionalEquivalences { get; }

    IEnumerable<UnresolvedIncludeUnitsDefinition> IInclusionExclusion<UnresolvedIncludeUnitsDefinition, UnresolvedExcludeUnitsDefinition>.IncludeUnits => IncludeUnits;
    IEnumerable<UnresolvedExcludeUnitsDefinition> IInclusionExclusion<UnresolvedIncludeUnitsDefinition, UnresolvedExcludeUnitsDefinition>.ExcludeUnits => ExcludeUnits;

    public ParsedBaseVector(DefinedType vectorType, MinimalLocation vectorLocation, SharpMeasuresVectorDefinition vectorDefinition,
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
