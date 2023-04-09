﻿//HintName: Area.Derivations.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class Area
{
    /// <summary>Constructs a new <see cref="global::Area"/>, derived from other quantities.</summary>
    /// <param name="length1">This <see cref="global::Length"/> is used to derive a <see cref="global::Area"/>.</param>
    /// <param name="length2">This <see cref="global::Length"/> is used to derive a <see cref="global::Area"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Area From(global::Length length1, global::Length length2)
    {
        global::System.ArgumentNullException.ThrowIfNull(length1);
        global::System.ArgumentNullException.ThrowIfNull(length2);

        return new(length1.Magnitude * length2.Magnitude);
    }

    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }, resulting in a <see cref="global::Length"/>.</summary>
    /// <param name="a">The first dividend of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <param name="b">The second divisor of { <paramref name="a"/> / <paramref name="b"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Length operator /(global::Area a, global::Length b)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a.Magnitude / b.Magnitude);
    }
}