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
    public Position3 Multiply(global::SharpMeasures.Vector3 factor) => new(Magnitude.Value * factor);

    /// <inheritdoc cref="global::SharpMeasures.Vector3.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Vector3)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static Position3 operator *(global::Length x, global::SharpMeasures.Vector3 y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude.Value * y);
    }

    /// <inheritdoc cref="global::SharpMeasures.Vector3.operator *(global::SharpMeasures.Vector3, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static Position3 operator *(global::SharpMeasures.Vector3 x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x * y.Magnitude.Value);
    }
}
