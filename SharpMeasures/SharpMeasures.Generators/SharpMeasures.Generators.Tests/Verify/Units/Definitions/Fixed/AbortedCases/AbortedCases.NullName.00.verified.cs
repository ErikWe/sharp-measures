﻿//HintName: UnitOfLength_Comparable.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

using System;

public partial class UnitOfLength : IComparable<UnitOfLength>
{
	public int CompareTo(UnitOfLength other) => Length.CompareTo(other.Length);

	public static bool operator <(UnitOfLength x, UnitOfLength y) => x.Length < y.Length;
	public static bool operator >(UnitOfLength x, UnitOfLength y) => x.Length > y.Length;
	public static bool operator <=(UnitOfLength x, UnitOfLength y) => x.Length <= y.Length;
	public static bool operator >=(UnitOfLength x, UnitOfLength y) => x.Length >= y.Length;
}