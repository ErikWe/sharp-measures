namespace SharpMeasures;

using SharpMeasures.Generators;

[FixedUnitInstance("Kilogram", "[*]s")]
[PrefixedUnitInstance("Gram", "[*]s", "Kilogram", MetricPrefixName.Milli)]
[PrefixedUnitInstance("Milligram", "[*]s", "Gram", MetricPrefixName.Milli)]
[PrefixedUnitInstance("Hectogram", "[*]s", "Gram", MetricPrefixName.Hecto)]
[PrefixedUnitInstance("Tonne", "[*]s", "Gram", MetricPrefixName.Mega)]
[ScaledUnitInstance("Ounce", "[*]s", "Gram", 28.349523125)]
[ScaledUnitInstance("Pound", "[*]s", "Ounce", 16)]
[Unit(typeof(Mass))]
public readonly partial record struct UnitOfMass { }
