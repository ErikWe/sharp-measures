namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("Joule", "[*]s", "Force * Length", new[] { "Newton", "Metre" })]
[PrefixedUnitInstance("Kilojoule", "[*]s", "Joule", MetricPrefixName.Kilo)]
[PrefixedUnitInstance("Megajoule", "[*]s", "Joule", MetricPrefixName.Mega)]
[PrefixedUnitInstance("Gigajoule", "[*]s", "Joule", MetricPrefixName.Giga)]
[DerivedUnitInstance("KilowattHour", "[*]s", "Power * Time", new[] { "Kilowatt", "Hour" })]
[ScaledUnitInstance("Calorie", "[*]s", "Joule", 4.184)]
[PrefixedUnitInstance("Kilocalorie", "[*]s", "Calorie", MetricPrefixName.Kilo)]
[DerivableUnit("Force * Length", "{0} * {1}", typeof(UnitOfForce), typeof(UnitOfLength), Permutations = true)]
[DerivableUnit("Power * Time", "{0} * {1}", typeof(UnitOfPower), typeof(UnitOfTime), Permutations = true)]
[Unit(typeof(Energy))]
public readonly partial record struct UnitOfEnergy { }
