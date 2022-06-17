namespace SharpMeasures.Generators.Vectors.Parsing.Abstractions;

using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal interface IRawParsedVector<TDefinition>
    where TDefinition : IVectorDefinition
{
    public abstract DefinedType VectorType { get; }
    public abstract MinimalLocation VectorLocation { get; }
    public abstract TDefinition VectorDefinition { get; }

    public abstract IEnumerable<RawIncludeUnitsDefinition> IncludeUnits { get; }
    public abstract IEnumerable<RawExcludeUnitsDefinition> ExcludeUnits { get; }

    public abstract IEnumerable<RawVectorConstantDefinition> VectorConstants { get; }
    public abstract IEnumerable<RawDimensionalEquivalenceDefinition> DimensionalEquivalences { get; }
}
