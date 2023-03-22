namespace SharpMeasures;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity, having only magnitude.</summary>
public interface IScalarQuantity
{
    /// <summary>The magnitude of the scalar quantity, expressed in some arbitrary unit.</summary>
    public abstract Scalar Magnitude { get; }
}

/// <inheritdoc cref="IScalarQuantity" path="/summary"/>
/// <typeparam name="TSelf">The self-type.</typeparam>
public interface IScalarQuantity<TSelf> : IScalarQuantity where TSelf : IScalarQuantity<TSelf>
{
    /// <summary>Constructs the <typeparamref name="TSelf"/> representing the provided <see cref="Scalar"/> magnitude, expressed in some arbitrary unit.</summary>
    /// <param name="magnitude">The magnitude of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <remarks>In most cases, this should be avoided - instead, construction of <typeparamref name="TSelf"/> should be done by also specifying the intended unit.</remarks>
    /// <returns>The constructed <typeparamref name="TSelf"/>, representing the provided <see cref="Scalar"/> magnitude.</returns>
    [SuppressMessage("Design", "CA1000: Do not declare static members on generic types", Justification = "Member is not accessed a generic fashion.")]
    public static abstract TSelf WithMagnitude(Scalar magnitude);
}
