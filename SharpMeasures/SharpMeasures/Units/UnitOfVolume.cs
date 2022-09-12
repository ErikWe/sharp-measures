namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("CubicMetre", "[*]s", new[] { "Metre" })]
[DerivedUnitInstance("CubicDecimetre", "[*]s", new[] { "Decimetre" })]
[DerivedUnitInstance("CubicCentimetre", "[*]s", new[] { "Centimetre" })]
[DerivedUnitInstance("CubicMillimetre", "[*]s", new[] { "Millimetre" })]
[DerivedUnitInstance("CubicKilometre", "[*]s", new[] { "Kilometre" })]
[DerivedUnitInstance("CubicInch", "[*]s", new[] { "Inch" })]
[DerivedUnitInstance("CubicFoot", "CubicFeet", new[] { "Foot" })]
[DerivedUnitInstance("CubicYard", "[*]s", new[] { "Yard" })]
[DerivedUnitInstance("CubicMile", "[*]s", new[] { "Mile" })]
[UnitInstanceAlias("Litre", "[*]s", "CubicDecimetre")]
[PrefixedUnitInstance("Decilitre", "[*]s", "Litre", MetricPrefixName.Deci)]
[PrefixedUnitInstance("Centilitre", "[*]s", "Litre", MetricPrefixName.Centi)]
[PrefixedUnitInstance("Millilitre", "[*]s", "Litre", MetricPrefixName.Milli)]
[DerivableUnit("{0} * {0} * {0}", typeof(UnitOfLength))]
[SharpMeasuresUnit(typeof(Volume))]
public readonly partial record struct UnitOfVolume { }
