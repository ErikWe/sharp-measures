﻿//HintName: UnitOfLength.Instances.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Units <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class UnitOfLength
{
    /// <summary>A <see cref="global::UnitOfLength"/>, describing a certain <see cref="global::Length"/>.</summary>
    public static global::UnitOfLength Metre { get; } = new(new global::Length(1));

    /// <summary>A <see cref="global::UnitOfLength"/>, describing a certain <see cref="global::Length"/>.</summary>
    public static global::UnitOfLength Kilometer { get; } = Metre.WithPrefix(global::SharpMeasures.MetricPrefix.Kilo);
}
