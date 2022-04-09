namespace SharpMeasures.Vector3Abstractions;

/// <summary>Describes a three-dimensional vector quantity that may be scaled by a <see cref="Scalar"/> or <see cref="double"/>.</summary>
/// <typeparam name="TScaledVector3Quantity">The three-dimensional vector quantity that is the scaled version of the original quantity,
/// generally <see langword="this"/>.</typeparam>
public interface IScalableVector3Quantity<out TScaledVector3Quantity> :
    IVector3Quantity
    where TScaledVector3Quantity : IVector3Quantity
{
    /// <summary>Scales the vector quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The vector quantity is scaled by this value.</param>
    public abstract TScaledVector3Quantity Multiply(double factor);
    /// <summary>Scales the vector quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The vector quantity is scaled by this value.</param>
    public abstract TScaledVector3Quantity Multiply(Scalar factor);

    /// <summary>Scales the vector quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The vector quantity is scaled through division by this value.</param>
    public abstract TScaledVector3Quantity Divide(double divisor);

    /// <summary>Scales the vector quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The vector quantity is scaled through division by this value.</param>
    public abstract TScaledVector3Quantity Divide(Scalar divisor);
}
