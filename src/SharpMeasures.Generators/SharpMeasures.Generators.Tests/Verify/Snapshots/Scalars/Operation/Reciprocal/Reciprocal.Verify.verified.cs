﻿//HintName: Time.Operations.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class Time
{
    /// <summary>Computes { <paramref name="scalar"/> / <see langword="this"/> }.</summary>
    /// <param name="scalar">The dividend of { <paramref name="scalar"/> / <see langword="this"/> }.</param>
    public global::Frequency DivideInto(global::SharpMeasures.Scalar scalar) => new(scalar / Magnitude);

    /// <summary>Computes { <see langword="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <see langword="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The divisor of { <see langword="a"/> / <paramref name="b"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Frequency operator /(global::SharpMeasures.Scalar a, global::Time b)
    {
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a / b.Magnitude);
    }
}