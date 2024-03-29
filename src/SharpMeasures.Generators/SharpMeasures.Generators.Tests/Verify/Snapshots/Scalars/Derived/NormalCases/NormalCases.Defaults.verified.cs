﻿//HintName: Time.Derivations.g.cs
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
    /// <summary>Constructs a new <see cref="global::Time"/>, derived from other quantities.</summary>
    /// <param name="scalar">This <see cref="global::SharpMeasures.Scalar"/> is used to derive a <see cref="global::Time"/>.</param>
    /// <param name="frequency">This <see cref="global::Frequency"/> is used to derive a <see cref="global::Time"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Time From(global::SharpMeasures.Scalar scalar, global::Frequency frequency)
    {
        global::System.ArgumentNullException.ThrowIfNull(frequency);

        return new(scalar / frequency.Magnitude);
    }

    /// <summary>Constructs a new <see cref="global::Time"/>, derived from other quantities.</summary>
    /// <param name="frequency">This <see cref="global::Frequency"/> is used to derive a <see cref="global::Time"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Time From(global::Frequency frequency)
    {
        global::System.ArgumentNullException.ThrowIfNull(frequency);

        return new(1 / frequency.Magnitude);
    }

    /// <summary>Computes { <paramref name="a"/> * <paramref name="b"/> }, resulting in a <see cref="global::SharpMeasures.Scalar"/>.</summary>
    /// <param name="a">The factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <param name="b">The factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Scalar operator *(global::Time a, global::Frequency b)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a.Magnitude * b.Magnitude);
    }

    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }, resulting in a <see cref="global::Frequency"/>.</summary>
    /// <param name="a">The first dividend of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The second divisor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Frequency operator /(global::SharpMeasures.Scalar a, global::Time b)
    {
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a / b.Magnitude);
    }
}
