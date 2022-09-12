namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("PerMillimetre", "[*]", new[] { "Millimetre" })]
[DerivedUnitInstance("PerCentimetre", "[*]", new[] { "Centimetre" })]
[DerivedUnitInstance("PerDecimetre", "[*]", new[] { "Decimetre" })]
[DerivedUnitInstance("PerMetre", "[*]", new[] { "Metre" })]
[DerivedUnitInstance("PerKilometre", "[*]", new[] { "Kilometre" })]
[DerivedUnitInstance("PerInch", "[*]", new[] { "Inch" })]
[DerivedUnitInstance("PerFoot", "[*]", new[] { "Foot" })]
[DerivedUnitInstance("PerYard", "[*]", new[] { "Yard" })]
[DerivedUnitInstance("PerMile", "[*]", new[] { "Mile" })]
[DerivableUnit("1 / {0}", typeof(UnitOfLength))]
[SharpMeasuresUnit(typeof(SpatialFrequency))]
public readonly partial record struct UnitOfSpatialFrequency { }
