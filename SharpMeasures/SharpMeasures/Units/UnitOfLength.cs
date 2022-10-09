namespace SharpMeasures;

using SharpMeasures.Generators;

[FixedUnitInstance("Metre", "[*]s")]
[PrefixedUnitInstance("Femtometre", "[*]s", "Metre", MetricPrefixName.Femto)]
[PrefixedUnitInstance("Picometre", "[*]s", "Metre", MetricPrefixName.Pico)]
[PrefixedUnitInstance("Nanometre", "[*]s", "Metre", MetricPrefixName.Nano)]
[PrefixedUnitInstance("Micrometre", "[*]s", "Metre", MetricPrefixName.Micro)]
[PrefixedUnitInstance("Millimetre", "[*]s", "Metre", MetricPrefixName.Milli)]
[PrefixedUnitInstance("Centimetre", "[*]s", "Metre", MetricPrefixName.Centi)]
[PrefixedUnitInstance("Decimetre", "[*]s", "Metre", MetricPrefixName.Deci)]
[PrefixedUnitInstance("Kilometre", "[*]s", "Metre", MetricPrefixName.Kilo)]
[ScaledUnitInstance("AstronomicalUnit", "[*]s", "Metre", 1.495978797E-11)]
[ScaledUnitInstance("LightYear", "[*]s", "Metre", 9460730472580800)]
[ScaledUnitInstance("Parsec", "[*]s", "AstronomicalUnit", "648000 / System.Math.PI")]
[PrefixedUnitInstance("Kiloparsec", "[*]s", "Parsec", MetricPrefixName.Kilo)]
[PrefixedUnitInstance("Megaparsec", "[*]s", "Parsec", MetricPrefixName.Mega)]
[PrefixedUnitInstance("Gigaparsec", "[*]s", "Parsec", MetricPrefixName.Giga)]
[ScaledUnitInstance("Inch", "[*]es", "Millimetre", 25.4)]
[ScaledUnitInstance("Foot", "Feet", "Inch", 12)]
[ScaledUnitInstance("Yard", "[*]s", "Foot", 3)]
[ScaledUnitInstance("Mile", "[*]s", "Yard", 1760)]
[ScaledUnitInstance("Angstrom", "[*]s", "Nanometre", 0.1)]
[ScaledUnitInstance("NauticalMile", "[*]s", "Metre", 1852)]
[Unit(typeof(Distance))]
public readonly partial record struct UnitOfLength { }
