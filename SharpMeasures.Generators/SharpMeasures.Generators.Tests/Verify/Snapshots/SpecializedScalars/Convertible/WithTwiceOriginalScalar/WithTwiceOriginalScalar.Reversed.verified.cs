﻿//HintName: Altitude.Conversions.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class Altitude
{
    /// <summary>Converts <see langword="this"/> to the equivalent <see cref="global::Distance"/>.</summary>
    public global::Distance AsDistance => new(Magnitude);

    /// <summary>Converts <paramref name="distance"/> to the equivalent <see cref="global::Altitude"/>.</summary>
    /// <param name="distance">This <see cref="global::Distance"/> is converted to the original <see cref="global::Altitude"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::Altitude From(global::Distance distance)
    {
        global::System.ArgumentNullException.ThrowIfNull(distance);

        return new(distance.Magnitude);
    }

    /// <summary>Converts <see langword="this"/> to the equivalent <see cref="global::Length"/>.</summary>
    public global::Length AsLength => new(Magnitude);

    /// <summary>Converts <paramref name="length"/> to the equivalent <see cref="global::Altitude"/>.</summary>
    /// <param name="length">This <see cref="global::Length"/> is converted to the original <see cref="global::Altitude"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::Altitude From(global::Length length)
    {
        global::System.ArgumentNullException.ThrowIfNull(length);

        return new(length.Magnitude);
    }

    /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="global::Distance"/>.</summary>
    /// <param name="x">This <see cref="global::Altitude"/> is converted to the equivalent <see cref="global::Distance"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static explicit operator global::Distance(global::Altitude x)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude);
    }

    /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="global::Altitude"/>.</summary>
    /// <param name="x">This <see cref="global::Distance"/> is converted to the equivalent <see cref="global::Altitude"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static implicit operator global::Altitude(global::Distance x)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude);
    }

    /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="global::Length"/>.</summary>
    /// <param name="x">This <see cref="global::Altitude"/> is converted to the equivalent <see cref="global::Length"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static explicit operator global::Length(global::Altitude x)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude);
    }

    /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="global::Altitude"/>.</summary>
    /// <param name="x">This <see cref="global::Length"/> is converted to the equivalent <see cref="global::Altitude"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static implicit operator global::Altitude(global::Length x)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude);
    }
}
