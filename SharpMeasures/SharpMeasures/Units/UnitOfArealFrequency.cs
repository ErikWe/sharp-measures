namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("PerSquareMetre", "[*]", new[] { "SquareMetre" })]
[DerivedUnitInstance("PerSquareDecimetre", "[*]", new[] { "SquareDecimetre" })]
[DerivedUnitInstance("PerSquareCentimetre", "[*]", new[] { "SquareCentimetre" })]
[DerivedUnitInstance("PerSquareMillimetre", "[*]", new[] { "SquareMillimetre" })]
[DerivedUnitInstance("PerSquareKilometre", "[*]", new[] { "SquareKilometre" })]
[DerivedUnitInstance("PerSquareInch", "[*]", new[] { "SquareInch" })]
[DerivedUnitInstance("PerSquareFoot", "[*]", new[] { "SquareFoot" })]
[DerivedUnitInstance("PerSquareYard", "[*]", new[] { "SquareYard" })]
[DerivedUnitInstance("PerSquareMile", "[*]", new[] { "SquareMile" })]
[DerivedUnitInstance("PerAre", "[*]", new[] { "Are" })]
[DerivedUnitInstance("PerHectare", "[*]", new[] { "Hectare" })]
[DerivedUnitInstance("PerAcre", "[*]", new[] { "Acre" })]
[DerivableUnit("1 / {0}", typeof(UnitOfArea))]
[SharpMeasuresUnit(typeof(ArealFrequency))]
public readonly partial record struct UnitOfArealFrequency { }
