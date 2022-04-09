namespace SharpMeasures;

/// <summary>Describes a scalar quantity, having only magnitude.</summary>
public interface IScalarQuantity
{
    /// <summary>The magnitude of the scalar quantity.</summary>
    public abstract double Magnitude { get; }
}