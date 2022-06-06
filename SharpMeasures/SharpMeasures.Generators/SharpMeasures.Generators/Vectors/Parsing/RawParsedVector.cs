namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using System;
using System.Collections.Generic;
using System.Linq;

internal record class RawParsedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public GeneratedVector VectorDefinition { get; }

    public EquatableEnumerable<RawIncludeUnits> IncludeUnits { get; }
    public EquatableEnumerable<RawExcludeUnits> ExcludeUnits { get; }

    public EquatableEnumerable<RawVectorConstant> VectorConstants { get; }
    public EquatableEnumerable<RawDimensionalEquivalence> DimensionalEquivalences { get; }

    public RawParsedVector(DefinedType vectorType, MinimalLocation vectorLocation, GeneratedVector vectorDefinition,
        IEnumerable<RawIncludeUnits> includeUnits, IEnumerable<RawExcludeUnits> excludeUnits, IEnumerable<RawVectorConstant> vectorConstants,
        IEnumerable<RawDimensionalEquivalence> dimensionalEquivalences)
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
