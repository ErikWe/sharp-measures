﻿//HintName: UnitOfLength_Misc.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class UnitOfLength
{
	public Length Length { get; }

	private UnitOfLength(Length length)
	{
		Length = length;
	}

	public UnitOfLength ScaledBy(SharpMeasures.Scalar scale) => ScaledBy(scale.Magnitude);
	public UnitOfLength ScaledBy(double scale) => new(Length * scale);

	public UnitOfLength WithPrefix(SharpMeasures.MetricPrefix prefix) => ScaledBy(prefix.Scale);

	public override string ToString() => $"{typeof(UnitOfLength)}: {Length}";
}