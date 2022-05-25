namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using System.Collections.Generic;

public interface IPoweredScalarDefinition
{
    public abstract NamedType Quantity { get; }
    public abstract IReadOnlyList<NamedType> SecondaryQuantities { get; }

    public abstract IPoweredScalarParsingData ParsingData { get; }
}
