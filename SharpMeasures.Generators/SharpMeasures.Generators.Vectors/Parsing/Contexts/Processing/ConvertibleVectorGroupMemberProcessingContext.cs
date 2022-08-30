namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

internal record class ConvertibleVectorGroupMemberProcessingContext : ConvertibleQuantityProcessingContext, IConvertibleVectorGroupMemberProcessingContext
{
    public NamedType Group { get; }

    public ConvertibleVectorGroupMemberProcessingContext(DefinedType type, NamedType group) : base(type)
    {
        Group = group;
    }
}
