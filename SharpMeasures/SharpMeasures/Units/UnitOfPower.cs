namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("Watt", "[*]s", new[] { "Joule", "Second" })]
[PrefixedUnitInstance("Kilowatt", "[*]s", "Watt", MetricPrefixName.Kilo)]
[PrefixedUnitInstance("Megawatt", "[*]s", "Watt", MetricPrefixName.Mega)]
[PrefixedUnitInstance("Gigawatt", "[*]s", "Watt", MetricPrefixName.Giga)]
[DerivableUnit("{0} / {1}", typeof(UnitOfEnergy), typeof(UnitOfTime))]
[Unit(typeof(Power))]
public readonly partial record struct UnitOfPower { }
