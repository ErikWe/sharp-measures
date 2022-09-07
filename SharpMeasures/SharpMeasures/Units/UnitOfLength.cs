namespace SharpMeasures;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[FixedUnitInstance("Metre", CommonPluralNotations.AppendS)]
[PrefixedUnitInstance("Femtometre", CommonPluralNotations.AppendS, "Metre", MetricPrefixName.Femto)]
[PrefixedUnitInstance("Picometre", CommonPluralNotations.AppendS, "Metre", MetricPrefixName.Pico)]
[PrefixedUnitInstance("Nanometre", CommonPluralNotations.AppendS, "Metre", MetricPrefixName.Nano)]
[PrefixedUnitInstance("Micrometre", CommonPluralNotations.AppendS, "Metre", MetricPrefixName.Micro)]
[PrefixedUnitInstance("Millimetre", CommonPluralNotations.AppendS, "Metre", MetricPrefixName.Milli)]
[PrefixedUnitInstance("Centimetre", CommonPluralNotations.AppendS, "Metre", MetricPrefixName.Centi)]
[PrefixedUnitInstance("Decimetre", CommonPluralNotations.AppendS, "Metre", MetricPrefixName.Deci)]
[PrefixedUnitInstance("Kilometre", CommonPluralNotations.AppendS, "Metre", MetricPrefixName.Kilo)]
[ScaledUnitInstance("AstronomicalUnit", CommonPluralNotations.AppendS, "Metre", 149597870700)]
[ScaledUnitInstance("LightYear", CommonPluralNotations.AppendS, "Metre", 9460730472580800)]
[ScaledUnitInstance("Parsec", CommonPluralNotations.AppendS, "AstronomicalUnit", "648000 / System.Math.PI")]
[PrefixedUnitInstance("Kiloparsec", CommonPluralNotations.AppendS, "Parsec", MetricPrefixName.Kilo)]
[PrefixedUnitInstance("Megaparsec", CommonPluralNotations.AppendS, "Parsec", MetricPrefixName.Mega)]
[PrefixedUnitInstance("Gigaparsec", CommonPluralNotations.AppendS, "Parsec", MetricPrefixName.Giga)]
[ScaledUnitInstance("Inch", CommonPluralNotations.AppendEs, "Millimetre", 25.4)]
[ScaledUnitInstance("Foot", "Feet", "Inch", 12)]
[SharpMeasuresUnit(typeof(Length), BiasTerm = false)]
public readonly partial record struct UnitOfLength { }
