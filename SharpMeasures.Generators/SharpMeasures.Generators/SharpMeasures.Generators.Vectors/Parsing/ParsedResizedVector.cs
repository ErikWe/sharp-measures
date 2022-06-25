namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Abstrations;
using SharpMeasures.Generators.Quantities.Parsing.Abstractions;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal class ParsedResizedVector : IInclusionExclusion<IncludeUnitsDefinition, ExcludeUnitsDefinition>
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public ResizedSharpMeasuresVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<IncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<ExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<VectorConstantDefinition> VectorConstants { get; }
    public EquatableEnumerable<DimensionalEquivalenceDefinition> DimensionalEquivalences { get; }

    IEnumerable<IncludeUnitsDefinition> IInclusionExclusion<IncludeUnitsDefinition, ExcludeUnitsDefinition>.IncludeUnits => IncludeUnits;
    IEnumerable<ExcludeUnitsDefinition> IInclusionExclusion<IncludeUnitsDefinition, ExcludeUnitsDefinition>.ExcludeUnits => ExcludeUnits;

    public ParsedResizedVector(DefinedType vectorType, MinimalLocation vectorLocation, ResizedSharpMeasuresVectorDefinition vectorDefinition,
        IEnumerable<IncludeUnitsDefinition> includeUnits, IEnumerable<ExcludeUnitsDefinition> excludeUnits, IEnumerable<VectorConstantDefinition> vectorConstants,
        IEnumerable<DimensionalEquivalenceDefinition> dimensionalEquivalences)
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
