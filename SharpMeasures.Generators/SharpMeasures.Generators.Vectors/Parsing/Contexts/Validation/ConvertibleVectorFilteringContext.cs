namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal record class ConvertibleVectorFilteringContext : SimpleProcessingContext, IConvertibleVectorFilteringContext
{
    public int Dimension { get; }
    public VectorType VectorType { get; }

    public IVectorPopulation VectorPopulation { get; }

    public HashSet<NamedType> InheritedConversions { get; }
    public HashSet<NamedType> ListedMatchingConversions { get; } = new();

    public ConvertibleVectorFilteringContext(DefinedType type, int dimension, VectorType vectorType, IVectorPopulation vectorPopulation, HashSet<NamedType> inheritedConversions) : base(type)
    {
        Dimension = dimension;
        VectorType = vectorType;

        VectorPopulation = vectorPopulation;

        InheritedConversions = inheritedConversions;
    }

    public ConvertibleVectorFilteringContext(DefinedType type, VectorType vectorType, IVectorPopulation vectorPopulation, HashSet<NamedType> inheritedConversions)
        : this(type, -1, vectorType, vectorPopulation, inheritedConversions) { }
}
