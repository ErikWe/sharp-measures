namespace SharpMeasures;

using SharpMeasures.Generators;

[FixedUnitInstance("Second", "[*]s")]
[ScaledUnitInstance("Minute", "[*]s", "Second", 60)]
[ScaledUnitInstance("Hour", "[*]s", "Minute", 60)]
[ScaledUnitInstance("Day", "[*]s", "Hour", 24)]
[ScaledUnitInstance("Week", "[*]s", "Day", 7)]
[ScaledUnitInstance("CommonYear", "[*]s", "Day", 365)]
[ScaledUnitInstance("JulianYear", "[*]s", "Day", 365.25)]
[PrefixedUnitInstance("Femtosecond", "[*]s", "Second", MetricPrefixName.Femto)]
[PrefixedUnitInstance("Picosecond", "[*]s", "Second", MetricPrefixName.Pico)]
[PrefixedUnitInstance("Nanosecond", "[*]s", "Second", MetricPrefixName.Nano)]
[PrefixedUnitInstance("Microsecond", "[*]s", "Second", MetricPrefixName.Micro)]
[PrefixedUnitInstance("Millisecond", "[*]s", "Second", MetricPrefixName.Milli)]
[Unit(typeof(Time))]
public readonly partial record struct UnitOfTime { }
