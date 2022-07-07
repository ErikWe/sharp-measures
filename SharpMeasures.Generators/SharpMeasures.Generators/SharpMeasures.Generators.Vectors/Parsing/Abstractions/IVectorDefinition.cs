namespace SharpMeasures.Generators.Vectors.Parsing.Abstractions;

using SharpMeasures.Generators.Attributes.Parsing;

internal interface IVectorDefinition : IOpenAttributeDefinition
{
    public abstract int Dimension { get; }
}
