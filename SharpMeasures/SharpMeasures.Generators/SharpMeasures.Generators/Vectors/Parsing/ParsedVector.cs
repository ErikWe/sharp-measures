namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Equatables;

using System.Collections.Generic;

internal record class ParsedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public GeneratedVector VectorDefinition { get; }

    public EquatableEnumerable<IncludeUnits> IncludeUnits { get; }
    public EquatableEnumerable<ExcludeUnits> ExcludeUnits { get; }

    public EquatableEnumerable<VectorConstant> VectorConstants { get; }
    public EquatableEnumerable<DimensionalEquivalence> DimensionalEquivalences { get; }

    public ParsedVector(DefinedType vectorType, MinimalLocation vectorLocation, GeneratedVector vectorDefinition, IEnumerable<IncludeUnits> includeUnits,
        IEnumerable<ExcludeUnits> excludeUnits, IEnumerable<VectorConstant> vectorConstants, IEnumerable<DimensionalEquivalence> dimensionalEquivalences)
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
