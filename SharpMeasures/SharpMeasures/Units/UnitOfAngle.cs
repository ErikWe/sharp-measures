namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[FixedUnitInstance("Radian", "[*]s")]
[PrefixedUnitInstance("Milliradian", "[*]s", "Radian", MetricPrefixName.Milli)]
[ScaledUnitInstance("Degree", "[*]s", "Radian", "System.Math.PI / 180")]
[ScaledUnitInstance("Gradian", "[*]s", "Radian", "System.Math.PI / 200")]
[ScaledUnitInstance("Arcminute", "[*]s", "Degree", 1d / 60)]
[ScaledUnitInstance("Arcsecond", "[*]s", "Arcminute", 1d / 60)]
[PrefixedUnitInstance("Milliarcsecond", "[*]s", "Arcsecond", MetricPrefixName.Milli)]
[PrefixedUnitInstance("Microarcsecond", "[*]s", "Arcsecond", MetricPrefixName.Micro)]
[ScaledUnitInstance("Turn", "[*]s", "Radian", "System.Math.Tau")]
[ScaledUnitInstance("Halfturn", "[*]s", "Turn", 0.5)]
[ScaledUnitInstance("QuarterTurn", "[*]", "Turn", 0.25)]
[PrefixedUnitInstance("Centiturn", "[*]s", "Turn", MetricPrefixName.Centi)]
[PrefixedUnitInstance("Milliturn", "[*]s", "Turn", MetricPrefixName.Milli)]
[ScaledUnitInstance("BinaryDegree", "[*]s", "Turn", 1d / 256)]
[SharpMeasuresUnit(typeof(Angle))]
public readonly partial record struct UnitOfAngle { }
