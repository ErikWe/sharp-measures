namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal record class ConvertibleVectorFilteringContext : SimpleProcessingContext, IConvertibleVectorFilteringContext
{
    public VectorType VectorType { get; }

    public IVectorPopulation VectorPopulation { get; }

    public HashSet<NamedType> InheritedConversions { get; }

    public ConvertibleVectorFilteringContext(DefinedType type, VectorType vectorType, IVectorPopulation vectorPopulation, HashSet<NamedType> inheritedConversions) : base(type)
    {
        VectorType = vectorType;

        VectorPopulation = vectorPopulation;

        InheritedConversions = inheritedConversions;
    }
}
