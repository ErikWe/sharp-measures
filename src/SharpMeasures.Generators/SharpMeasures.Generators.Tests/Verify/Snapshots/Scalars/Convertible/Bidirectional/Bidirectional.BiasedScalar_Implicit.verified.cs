﻿//HintName: Temperature2.Conversions.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class Temperature2
{
    /// <summary>Converts <see langword="this"/> to the equivalent <see cref="global::Temperature"/>.</summary>
    public global::Temperature AsTemperature => new(Magnitude);

    /// <summary>Converts <paramref name="temperature"/> to the equivalent <see cref="global::Temperature2"/>.</summary>
    /// <param name="temperature">This <see cref="global::Temperature"/> is converted to the original <see cref="global::Temperature2"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::Temperature2 From(global::Temperature temperature)
    {
        global::System.ArgumentNullException.ThrowIfNull(temperature);

        return new(temperature.Magnitude);
    }

    /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="global::Temperature"/>.</summary>
    /// <param name="x">This <see cref="global::Temperature2"/> is converted to the equivalent <see cref="global::Temperature"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static implicit operator global::Temperature(global::Temperature2 x)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude);
    }

    /// <summary>Converts <paramref name="x"/> to the equivalent <see cref="global::Temperature2"/>.</summary>
    /// <param name="x">This <see cref="global::Temperature"/> is converted to the equivalent <see cref="global::Temperature2"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static implicit operator global::Temperature2(global::Temperature x)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude);
    }
}
