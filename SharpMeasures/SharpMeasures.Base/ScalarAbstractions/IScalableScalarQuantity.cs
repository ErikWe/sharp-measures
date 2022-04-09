namespace SharpMeasures.ScalarAbstractions;

/// <summary>Describes a scalar quantity that may be scaled by a <see cref="Scalar"/> or <see cref="double"/>.</summary>
/// <typeparam name="TScaledScalarQuantity">The scalar quantity that is the scaled version of the original quantity, generally <see langword="this"/>.</typeparam>
public interface IScalableScalarQuantity<out TScaledScalarQuantity> :
    IScalarQuantity
    where TScaledScalarQuantity : IScalarQuantity
{
    /// <summary>Scales the scalar quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The scalar quantity is scaled by this value.</param>
    public abstract TScaledScalarQuantity Multiply(double factor);
    /// <summary>Scales the scalar quantity by <paramref name="factor"/>.</summary>
    /// <param name="factor">The scalar quantity is scaled by this value.</param>
    public abstract TScaledScalarQuantity Multiply(Scalar factor);

    /// <summary>Scales the scalar quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The scalar quantity is scaled through division by this value.</param>
    public abstract TScaledScalarQuantity Divide(double divisor);

    /// <summary>Scales the scalar quantity through division by <paramref name="divisor"/>.</summary>
    /// <param name="divisor">The scalar quantity is scaled through division by this value.</param>
    public abstract TScaledScalarQuantity Divide(Scalar divisor);
}