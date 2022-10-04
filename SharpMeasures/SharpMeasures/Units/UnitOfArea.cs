namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("SquareMetre", "[*]s", new[] { "Metre" })]
[DerivedUnitInstance("SquareDecimetre", "[*]s", new[] { "Decimetre" })]
[DerivedUnitInstance("SquareCentimetre", "[*]s", new[] { "Centimetre" })]
[DerivedUnitInstance("SquareMillimetre", "[*]s", new[] { "Millimetre" })]
[DerivedUnitInstance("SquareKilometre", "[*]s", new[] { "Kilometre" })]
[DerivedUnitInstance("SquareInch", "[*]es", new[] { "Inch" })]
[DerivedUnitInstance("SquareFoot", "SquareFeet", new[] { "Foot" })]
[DerivedUnitInstance("SquareYard", "[*]s", new[] { "Yard" })]
[DerivedUnitInstance("SquareMile", "[*]s", new[] { "Mile" })]
[ScaledUnitInstance("Are", "[*]s", "SquareMetre", 100)]
[PrefixedUnitInstance("Hectare", "[*]s", "Are", MetricPrefixName.Hecto)]
[ScaledUnitInstance("Acre", "[*]s", "SquareMile", 1d / 640)]
[DerivableUnit("{0} * {0}", typeof(UnitOfLength))]
[Unit(typeof(Area))]
public readonly partial record struct UnitOfArea { }
