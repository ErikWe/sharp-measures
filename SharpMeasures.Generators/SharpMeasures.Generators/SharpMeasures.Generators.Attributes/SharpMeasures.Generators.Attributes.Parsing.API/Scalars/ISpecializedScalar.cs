namespace SharpMeasures.Generators.Scalars;

public interface ISpecializedScalar : IScalar
{
    public abstract IScalarType OriginalScalar { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConstants { get; }
    public abstract bool InheritBases { get; }
    public abstract bool InheritUnits { get; }
    public abstract bool InheritConvertibleQuantities { get; }
}
