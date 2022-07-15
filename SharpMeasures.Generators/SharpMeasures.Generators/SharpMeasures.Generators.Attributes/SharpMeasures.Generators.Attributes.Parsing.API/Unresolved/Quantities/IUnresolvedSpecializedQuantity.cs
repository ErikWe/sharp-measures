namespace SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedSpecializedQuantity
{
    public abstract NamedType OriginalQuantity { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritConvertibleQuantities { get; }
    public abstract bool InheritUnits { get; }
}
