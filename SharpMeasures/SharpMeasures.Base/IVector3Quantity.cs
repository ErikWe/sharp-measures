﻿namespace SharpMeasures;
/// <summary>Describes a three-dimensional vector quantity.</summary>
public interface IVector3Quantity
{
    /// <summary>The magnitude of the X-component of the vector quantity.</summary>
    public abstract double MagnitudeX { get; }
    /// <summary>The magnitude of the Y-component of the vector quantity.</summary>
    public abstract double MagnitudeY { get; }
    /// <summary>The magnitude of the Z-component of the vector quantity.</summary>
    public abstract double MagnitudeZ { get; }

    /// <summary>Computes the magnitude, or norm, of the vector quantity.</summary>
    /// <remarks>For improved performance, consider preferring <see cref="SquaredMagnitude"/> when applicable.</remarks>
    public abstract Scalar Magnitude();
    /// <summary>Computes the square of the magnitude, or norm, of the vector quantity.</summary>
    public abstract Scalar SquaredMagnitude();
}