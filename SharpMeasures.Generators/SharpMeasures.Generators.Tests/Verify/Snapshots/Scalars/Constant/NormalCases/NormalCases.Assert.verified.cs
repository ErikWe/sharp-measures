﻿//HintName: Length_Units.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class Length
{
    /// <summary>The <see cref="global::Length"/> representing the constant Planck, with value { 1.616E-35 <see cref="global::UnitOfLength.Metre"/> }.</summary>
    public static global::Length Planck => new(1.616255E-35, global::UnitOfLength.Metre);

    /// <summary>The <see cref="global::Length"/> representing { 1 <see cref="global::UnitOfLength.Metre"/> }.</summary>
    public static global::Length OneMetre => global::UnitOfLength.Metre.Length;

    /// <summary>The magnitude of <see langword="this", expressed in multiples of <see cref="global::Length.Planck"/>.</summary>
    public global::SharpMeasures.Scalar MultiplesOfPlanck => Magnitude.Value / Planck.Magnitude.Value;

    /// <summary>The magnitude of <see langword="this"/>, expressed in <see cref="global::UnitOfLength.Metre"/>.</summary>
    public global::SharpMeasures.Scalar Metres => InUnit(global::UnitOfLength.Metre);
}
