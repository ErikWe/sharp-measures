﻿//HintName: Length.Vectors.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class Length
{
    /// <inheritdoc cref="Scalar.Multiply(global::SharpMeasures.Vector3)"/>
    public global::Position3 Multiply(global::SharpMeasures.Vector3 factor) => new(Magnitude.Value * factor);

    /// <inheritdoc cref="global::SharpMeasures.Vector3.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Vector3)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Position3 operator *(global::Length a, global::SharpMeasures.Vector3 b)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);

        return new(a.Magnitude.Value * b);
    }

    /// <inheritdoc cref="global::SharpMeasures.Vector3.operator *(global::SharpMeasures.Vector3, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Position3 operator *(global::SharpMeasures.Vector3 a, global::Length b)
    {
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a * b.Magnitude.Value);
    }

    /// <inheritdoc cref="Scalar.Multiply(global::SharpMeasures.Vector2)"/>
    public global::Position2 Multiply(global::SharpMeasures.Vector2 factor) => new(Magnitude.Value * factor);

    /// <inheritdoc cref="global::SharpMeasures.Vector2.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Vector2)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Position2 operator *(global::Length a, global::SharpMeasures.Vector2 b)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);

        return new(a.Magnitude.Value * b);
    }

    /// <inheritdoc cref="global::SharpMeasures.Vector2.operator *(global::SharpMeasures.Vector2, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Position2 operator *(global::SharpMeasures.Vector2 a, global::Length b)
    {
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a * b.Magnitude.Value);
    }
}
