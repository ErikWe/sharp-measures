namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class RawParsedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public GeneratedVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<RawIncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<RawExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<RawVectorConstantDefinition> VectorConstants { get; }
    public EquatableEnumerable<RawDimensionalEquivalenceDefinition> DimensionalEquivalences { get; }

    public RawParsedVector(DefinedType vectorType, MinimalLocation vectorLocation, GeneratedVectorDefinition vectorDefinition,
        IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits, IEnumerable<RawVectorConstantDefinition> vectorConstants,
        IEnumerable<RawDimensionalEquivalenceDefinition> dimensionalEquivalences)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;

        IncludeUnits = new(includeUnits);
        ExcludeUnits = new(excludeUnits);

        VectorConstants = new(vectorConstants);
        DimensionalEquivalences = new(dimensionalEquivalences);
    }
}
