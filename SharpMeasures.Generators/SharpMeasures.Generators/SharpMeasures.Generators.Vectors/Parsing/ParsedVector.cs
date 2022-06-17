namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.Abstractions;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class ParsedVector : IUnitListInclusionExclusion<IncludeUnitsDefinition, ExcludeUnitsDefinition>
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public SharpMeasuresVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<IncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<ExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<VectorConstantDefinition> VectorConstants { get; }
    public EquatableEnumerable<DimensionalEquivalenceDefinition> DimensionalEquivalences { get; }

    IEnumerable<IncludeUnitsDefinition> IUnitListInclusionExclusion<IncludeUnitsDefinition, ExcludeUnitsDefinition>.IncludeUnits => IncludeUnits;
    IEnumerable<ExcludeUnitsDefinition> IUnitListInclusionExclusion<IncludeUnitsDefinition, ExcludeUnitsDefinition>.ExcludeUnits => ExcludeUnits;

    public ParsedVector(DefinedType vectorType, MinimalLocation vectorLocation, SharpMeasuresVectorDefinition vectorDefinition,
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
