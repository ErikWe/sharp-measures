namespace SharpMeasures;

using SharpMeasures.Generators;

[FixedUnitInstance("Kilogram", "[*]s")]
[PrefixedUnitInstance("Gram", "[*]s", "Kilogram", MetricPrefixName.Milli)]
[PrefixedUnitInstance("Milligram", "[*]s", "Gram", MetricPrefixName.Milli)]
[PrefixedUnitInstance("Microgram", "[*]s", "Gram", MetricPrefixName.Micro)]
[PrefixedUnitInstance("Hectogram", "[*]s", "Gram", MetricPrefixName.Hecto)]
[ScaledUnitInstance("Tonne", "[*]s", "Kilogram", 1000)]
[ScaledUnitInstance("Ounce", "[*]s", "Gram", 28.349523125)]
[ScaledUnitInstance("Pound", "[*]s", "Ounce", 16)]
[Unit(typeof(Mass))]
public readonly partial record struct UnitOfMass { }
