namespace SharpMeasures;

/// <summary>Describes a prefix, possessing a certain scale-factor.</summary>
public interface IPrefix
{
    /// <summary>The scale-factor of <see langword="this"/>.</summary>
    public abstract Scalar Factor { get; }
}
