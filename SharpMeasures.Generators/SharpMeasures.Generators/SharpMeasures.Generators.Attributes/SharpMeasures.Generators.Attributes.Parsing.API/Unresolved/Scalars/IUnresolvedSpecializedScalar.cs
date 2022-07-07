namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Scalars;

public interface IUnresolvedSpecializedScalar : IUnresolvedScalar
{
    new public abstract ISpecializedScalar UnresolvedTarget { get; }

    public abstract NamedType OriginalScalar { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritBases { get; }
    public abstract bool InheritUnits { get; }
    public abstract bool InheritConvertibleQuantities { get; }
}
