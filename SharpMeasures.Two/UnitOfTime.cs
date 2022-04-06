namespace ErikWe.SharpMeasures;

using ErikWe.SharpMeasures.Attributes;
using ErikWe.SharpMeasures.Attributes.Utility;

using System;

[Unit(typeof(Length))]
[FixedUnitInstance("Metre", UnitPluralCodes.AppendS, 1)]
public readonly partial record struct UnitOfLength { }

[Unit(typeof(Time))]
[FixedUnitInstance("Second", UnitPluralCodes.AppendS, 1)]
public readonly partial record struct UnitOfTime { }

[Unit(typeof(Speed))]
[DerivedUnit(UnitDerivations.Division, typeof(UnitOfLength), typeof(UnitOfTime))]
[DerivedUnitInstance("MetrePerSecond", UnitPluralCodes.InsertSBeforePer, new Type[] { typeof(UnitOfLength), typeof(UnitOfTime) }, new string[] { "Metre", "Second" })]
public readonly partial record struct UnitOfSpeed { }