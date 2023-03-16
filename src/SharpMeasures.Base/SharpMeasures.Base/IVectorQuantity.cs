namespace SharpMeasures;

/// <summary>Describes a vector quantity.</summary>
public interface IVectorQuantity
{
    /// <summary>Computes the magnitude, or length, of the vector quantity.</summary>
    /// <remarks>For improved performance, prefer <see cref="SquaredMagnitude"/> when applicable.</remarks>
    /// <returns>The magnitude of the vector quantity.</returns>
    public abstract Scalar Magnitude();

    /// <summary>Computes the square of the magnitude, or length, of the vector quantity.</summary>
    /// <returns>The squared magnitude of the vector quantity.</returns>
    public abstract Scalar SquaredMagnitude();
}
