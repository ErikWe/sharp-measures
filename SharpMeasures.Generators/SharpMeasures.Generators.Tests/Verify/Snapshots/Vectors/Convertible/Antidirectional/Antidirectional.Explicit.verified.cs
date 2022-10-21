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
    /// <summary>Converts <paramref name="position"/> to the equivalent <see cref="global::Displacement3"/>.</summary>
    /// <param name="position">This <see cref="global::Position3"/> is converted to the equivalent <see cref="global::Displacement3"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::Displacement3 From(global::Position3 position)
    {
        global::System.ArgumentNullException.ThrowIfNull(position);

        return new(position.Components);
    }

    /// <summary>Converts <paramref name="a"/> to the equivalent <see cref="global::Displacement3"/>.</summary>
    /// <param name="a">This <see cref="global::Position3"/> is converted to the equivalent <see cref="global::Displacement3"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static explicit operator global::Displacement3(global::Position3 a)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);

        return new(a.Components);
    }
}
