namespace SharpMeasures;

/// <summary>Describes a prefix, scaling quantities using a <see cref="Scalar"/> factor.</summary>
public interface IPrefix
{
    /// <summary>The scale-factor of the <see cref="IPrefix"/>.</summary>
    public abstract Scalar Factor { get; }
}
