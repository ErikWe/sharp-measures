namespace SharpMeasures;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.Utility;

[FixedUnit("Metre", UnitPluralCodes.AppendS, 1)]
[PrefixedUnit("Femtometre", UnitPluralCodes.AppendS, "Metre", MetricPrefixName.Femto)]
[PrefixedUnit("Picometre", UnitPluralCodes.AppendS, "Metre", MetricPrefixName.Pico)]
[PrefixedUnit("Nanometre", UnitPluralCodes.AppendS, "Metre", MetricPrefixName.Nano)]
[PrefixedUnit("Micrometre", UnitPluralCodes.AppendS, "Metre", MetricPrefixName.Micro)]
[PrefixedUnit("Millimetre", UnitPluralCodes.AppendS, "Metre", MetricPrefixName.Milli)]
[PrefixedUnit("Centimetre", UnitPluralCodes.AppendS, "Metre", MetricPrefixName.Centi)]
[PrefixedUnit("Decimetre", UnitPluralCodes.AppendS, "Metre", MetricPrefixName.Deci)]
[PrefixedUnit("Kilometre", UnitPluralCodes.AppendS, "Metre", MetricPrefixName.Kilo)]
[ScaledUnit("AstronomicalUnit", UnitPluralCodes.AppendS, "Metre", 149597870700)]
[ScaledUnit("LightYear", UnitPluralCodes.AppendS, "Metre", 9460730472580800)]
[ScaledUnit("Parsec", UnitPluralCodes.AppendS, "AstronomicalUnit", "648000 / System.Math.PI")]
[PrefixedUnit("Kiloparsec", UnitPluralCodes.AppendS, "Parsec", MetricPrefixName.Kilo)]
[PrefixedUnit("Megaparsec", UnitPluralCodes.AppendS, "Parsec", MetricPrefixName.Mega)]
[PrefixedUnit("Gigaparsec", UnitPluralCodes.AppendS, "Parsec", MetricPrefixName.Giga)]
[ScaledUnit("Inch", UnitPluralCodes.AppendEs, "Millimetre", 25.4)]
[ScaledUnit("Foot", "Feet", "Inch", 12)]
[SharpMeasuresUnit(typeof(Length), BiasTerm = false)]
public readonly partial record struct UnitOfLength { }
