namespace SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IConvertibleQuantity : IAttributeDefinition
{
    public abstract IReadOnlyList<NamedType> Quantities { get; }

    public abstract QuantityConversionDirection ConversionDirection { get; }
    public abstract ConversionOperatorBehaviour CastOperatorBehaviour { get; }

    new public abstract IConvertibleQuantityLocations Locations { get; }
}

public interface IConvertibleQuantityLocations : IAttributeLocations
{
    public abstract MinimalLocation? QuantitiesCollection { get; }
    public abstract IReadOnlyList<MinimalLocation> QuantitiesElements { get; }

    public abstract MinimalLocation? ConversionDirection { get; }
    public abstract MinimalLocation? CastOperatorBehaviour { get; }

    public abstract bool ExplicitlySetQuantities { get; }
    public abstract bool ExplicitlySetConversionDirection { get; }
    public abstract bool ExplicitlySetCastOperatorBehaviour { get; }
}
