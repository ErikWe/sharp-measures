namespace SharpMeasures;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity, consisting of X, Y, and Z components.</summary>
public interface IVector3Quantity : IVectorQuantity
{
    /// <summary>The magnitude of the X-component of the quantity, expressed in some arbitrary unit.</summary>
    public abstract Scalar X { get; }

    /// <summary>The magnitude of the Y-component of the quantity, expressed in some arbitrary unit.</summary>
    public abstract Scalar Y { get; }

    /// <summary>The magnitude of the Z-component of the quantity, expressed in some arbitrary unit.</summary>
    public abstract Scalar Z { get; }

    /// <summary>The magnitudes of the X, Y, and Z components of the quantity, expressed in some arbitrary unit.</summary>
    public abstract Vector3 Components { get; }
}

/// <inheritdoc cref="IVector3Quantity" path="/summary"/>
/// <typeparam name="TSelf">The self-type.</typeparam>
public interface IVector3Quantity<TSelf> : IVector3Quantity where TSelf : IVector3Quantity<TSelf>
{
    /// <summary>Constructs the <typeparamref name="TSelf"/> representing the provided <see cref="Scalar"/> components, expressed in some arbitrary unit.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <remarks>In most cases, this should be avoided - instead, construction of <typeparamref name="TSelf"/> should be done by also specifying the intended unit.</remarks>
    /// <returns>The constructed <typeparamref name="TSelf"/>, representing the provided <see cref="Scalar"/> components.</returns>
    [SuppressMessage("Design", "CA1000: Do not declare static members on generic types", Justification = "Member is not accessed a generic fashion.")]
    public static abstract TSelf WithComponents(Scalar x, Scalar y, Scalar z);

    /// <summary>Constructs the <typeparamref name="TSelf"/> representing the provided <see cref="Vector2"/> components, expressed in some arbitrary unit.</summary>
    /// <param name="components">The magnitudes of the X, Y, and Z components of the constructed <typeparamref name="TSelf"/>, expressed in some arbitrary unit.</param>
    /// <remarks>In most cases, this should be avoided - instead, construction of <typeparamref name="TSelf"/> should be done by also specifying the intended unit.</remarks>
    /// <returns>The constructed <typeparamref name="TSelf"/>, representing the provided <see cref="Vector2"/> components.</returns>
    [SuppressMessage("Design", "CA1000: Do not declare static members on generic types", Justification = "Member is not accessed a generic fashion.")]
    public static abstract TSelf WithComponents(Vector3 components);
}
