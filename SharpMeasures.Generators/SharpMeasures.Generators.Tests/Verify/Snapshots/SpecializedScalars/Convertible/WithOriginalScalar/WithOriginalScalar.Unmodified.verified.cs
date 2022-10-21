﻿//HintName: Distance.Conversions.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class Distance
{
    /// <summary>Converts <see langword="this"/> to the equivalent <see cref="global::Length"/>.</summary>
    public global::Length AsLength => new(Magnitude);

    /// <summary>Converts <paramref name="length"/> to the equivalent <see cref="global::Distance"/>.</summary>
    /// <param name="length">This <see cref="global::Length"/> is converted to the original <see cref="global::Distance"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::Distance From(global::Length length)
    {
        global::System.ArgumentNullException.ThrowIfNull(length);

        return new(length.Magnitude);
    }

    /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="global::Length"/>.</summary>
    /// <param name="x">This <see cref="global::Distance"/> is converted to the equivalent <see cref="global::Length"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static implicit operator global::Length(global::Distance x)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude);
    }

    /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="global::Distance"/>.</summary>
    /// <param name="x">This <see cref="global::Length"/> is converted to the equivalent <see cref="global::Distance"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static explicit operator global::Distance(global::Length x)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude);
    }
}
