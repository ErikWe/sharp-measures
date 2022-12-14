namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("PerSecond", "[*]", new[] { "Second" })]
[DerivedUnitInstance("PerMinute", "[*]", new[] { "Minute" })]
[DerivedUnitInstance("PerHour", "[*]", new[] { "Hour" })]
[UnitInstanceAlias("Hertz", "[*]", "PerSecond")]
[PrefixedUnitInstance("Kilohertz", "[*]", "Hertz", MetricPrefixName.Kilo)]
[PrefixedUnitInstance("Megahertz", "[*]", "Hertz", MetricPrefixName.Mega)]
[PrefixedUnitInstance("Gigahertz", "[*]", "Hertz", MetricPrefixName.Giga)]
[DerivableUnit("1 / {0}", typeof(UnitOfTime))]
[Unit(typeof(Frequency))]
public readonly partial record struct UnitOfFrequency { }
