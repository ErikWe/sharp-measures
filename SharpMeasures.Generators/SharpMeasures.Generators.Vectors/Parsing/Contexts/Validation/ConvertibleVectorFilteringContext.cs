namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal sealed record class ConvertibleVectorFilteringContext : IConvertibleVectorFilteringContext
{
    public DefinedType Type { get; }

    public int Dimension { get; }
    public VectorType VectorType { get; }

    public IVectorPopulation VectorPopulation { get; }

    public HashSet<NamedType> InheritedConversions { get; }
    public HashSet<NamedType> ListedMatchingConversions { get; } = new();

    public ConvertibleVectorFilteringContext(DefinedType type, VectorType vectorType, IVectorPopulation vectorPopulation, HashSet<NamedType> inheritedConversions) : this(type, -1, vectorType, vectorPopulation, inheritedConversions) { }
    public ConvertibleVectorFilteringContext(DefinedType type, int dimension, VectorType vectorType, IVectorPopulation vectorPopulation, HashSet<NamedType> inheritedConversions)
    {
        Type = type;

        Dimension = dimension;
        VectorType = vectorType;

        VectorPopulation = vectorPopulation;

        InheritedConversions = inheritedConversions;
    }
}
