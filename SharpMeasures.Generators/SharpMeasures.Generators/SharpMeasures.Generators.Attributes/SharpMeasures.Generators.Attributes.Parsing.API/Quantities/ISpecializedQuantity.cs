namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface ISpecializedQuantity : IQuantity
{
    public abstract IUnresolvedQuantityType OriginalQuantity { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritConvertibleQuantities { get; }
    public abstract bool InheritUnits { get; }
}
