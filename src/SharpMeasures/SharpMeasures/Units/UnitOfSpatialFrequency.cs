namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("PerMetre", "[*]", new[] { "Metre" })]
[DerivedUnitInstance("PerDecimetre", "[*]", new[] { "Decimetre" })]
[DerivedUnitInstance("PerCentimetre", "[*]", new[] { "Centimetre" })]
[DerivedUnitInstance("PerMillimetre", "[*]", new[] { "Millimetre" })]
[DerivedUnitInstance("PerKilometre", "[*]", new[] { "Kilometre" })]
[DerivedUnitInstance("PerInch", "[*]", new[] { "Inch" })]
[DerivedUnitInstance("PerFoot", "[*]", new[] { "Foot" })]
[DerivedUnitInstance("PerYard", "[*]", new[] { "Yard" })]
[DerivedUnitInstance("PerMile", "[*]", new[] { "Mile" })]
[DerivableUnit("1 / {0}", typeof(UnitOfLength))]
[Unit(typeof(SpatialFrequency))]
public readonly partial record struct UnitOfSpatialFrequency { }
