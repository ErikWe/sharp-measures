﻿//HintName: Displacement3.Conversions.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Vectors <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class Displacement3
{
    /// <summary>Converts <see langword="this"/> to the equivalent <see cref="Position3"/>.</summary>
    public global::Position3 AsPosition3 => new(Components);

    /// <summary>Converts <paramref name="position"/> to the equivalent <see cref="global::Displacement3"/>.</summary>
    /// <param name="a">This <see cref="global::Position3"/> is converted to the equivalent <see cref="global::Displacement3"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public Displacement3 From(global::Position3 position)
    {
        global::System.ArgumentNullException.ThrowIfNull(position);

        return new(position.Components);
    }

    /// <summary>Converts <paramref name="a"/> to the equivalent <see cref="Position3"/>.</summary>
    /// <param name="a">This <see cref="global::Displacement3"/> is converted to the equivalent <see cref="Position3"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static implicit operator Position3(global::Displacement3 a)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);

        return new(a.Components);
    }

    /// <summary>Converts <paramref name="a"/> to the equivalent <see cref="global::Displacement3"/>.</summary>
    /// <param name="a">This <see cref="global::Position3"/> is converted to the equivalent <see cref="global::Displacement3"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static explicit operator Displacement3(global::Position3 a)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);

        return new(a.Components);
    }
}
