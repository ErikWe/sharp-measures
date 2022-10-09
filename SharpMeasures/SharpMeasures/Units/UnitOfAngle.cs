namespace SharpMeasures;

using SharpMeasures.Generators;

[FixedUnitInstance("Radian", "[*]s")]
[PrefixedUnitInstance("Milliradian", "[*]s", "Radian", MetricPrefixName.Milli)]
[ScaledUnitInstance("Degree", "[*]s", "Radian", "System.Math.PI / 180")]
[ScaledUnitInstance("Gradian", "[*]s", "Radian", "System.Math.PI / 200")]
[ScaledUnitInstance("Arcminute", "[*]s", "Degree", 1d / 60)]
[ScaledUnitInstance("Arcsecond", "[*]s", "Arcminute", 1d / 60)]
[PrefixedUnitInstance("Milliarcsecond", "[*]s", "Arcsecond", MetricPrefixName.Milli)]
[PrefixedUnitInstance("Microarcsecond", "[*]s", "Arcsecond", MetricPrefixName.Micro)]
[ScaledUnitInstance("Revolution", "[*]s", "Radian", "System.Math.Tau")]
[ScaledUnitInstance("HalfRevolution", "[*]s", "Revolution", 0.5)]
[ScaledUnitInstance("QuarterRevolution", "[*]", "Revolution", 0.25)]
[ScaledUnitInstance("BinaryDegree", "[*]s", "Revolution", 1d / 256)]
[Unit(typeof(Angle))]
public readonly partial record struct UnitOfAngle { }
