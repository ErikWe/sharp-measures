﻿namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("Joule", "[*]s", "Force * Length", "Newton", "Metre")]
[PrefixedUnitInstance("Kilojoule", "[*]s", "Joule", MetricPrefixName.Kilo)]
[PrefixedUnitInstance("Megajoule", "[*]s", "Joule", MetricPrefixName.Mega)]
[PrefixedUnitInstance("Gigajoule", "[*]s", "Joule", MetricPrefixName.Giga)]
[DerivedUnitInstance("KilowattHour", "[*]s", "Power * Time", "Kilowatt", "Hour")]
[ScaledUnitInstance("Calorie", "[*]s", "Joule", 4.184)]
[PrefixedUnitInstance("Kilocalorie", "[*]s", "Calorie", MetricPrefixName.Kilo)]
[DerivableUnit("Force * Length", "{0} * {1}", typeof(UnitOfForce), typeof(UnitOfLength), Permutations = true)]
[DerivableUnit("Power * Time", "{0} * {1}", typeof(UnitOfPower), typeof(UnitOfTime), Permutations = true)]
[SharpMeasuresUnit(typeof(Energy))]
public readonly partial record struct UnitOfEnergy { }