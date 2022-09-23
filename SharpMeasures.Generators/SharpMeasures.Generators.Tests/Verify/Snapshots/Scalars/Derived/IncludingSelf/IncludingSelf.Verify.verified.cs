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
    /// <param name="timeSquared">This <see cref="global::TimeSquared"/> is used to derive a <see cref="global::Time"/>.</param>
    /// <param name="time">This <see cref="global::Time"/> is used to derive a <see cref="global::Time"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Time From(global::TimeSquared timeSquared, global::Time time)
    {
        global::System.ArgumentNullException.ThrowIfNull(timeSquared);
        global::System.ArgumentNullException.ThrowIfNull(time);

        return new(timeSquared.Magnitude / time.Magnitude);
    }

    /// <summary>Computes { <paramref name="a"/> * <paramref name="b"/> }, resulting in a <see cref="global::TimeSquared"/>.</summary>
    /// <param name="a">The factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <param name="b">The factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::TimeSquared operator *(global::Time a, global::Time b)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a.Magnitude * b.Magnitude);
    }

    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }, resulting in a <see cref="global::Time"/>.</summary>
    /// <param name="a">The first dividend of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The second divisor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Time operator /(global::TimeSquared a, global::Time b)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a.Magnitude / b.Magnitude);
    }
}
