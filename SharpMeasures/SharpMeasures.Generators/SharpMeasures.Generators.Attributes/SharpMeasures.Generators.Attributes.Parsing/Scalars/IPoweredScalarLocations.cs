namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using System.Collections.Generic;

public interface IPoweredScalarLocations
{
    public abstract MinimalLocation Quantity { get; }

    public abstract MinimalLocation SecondaryQuantitiesCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> SecondaryQuantitiesElements { get; }
}
