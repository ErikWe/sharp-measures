namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("PerCubicMetre", "[*]", new[] { "CubicMetre" })]
[DerivedUnitInstance("PerCubicDecimetre", "[*]", new[] { "CubicDecimetre" })]
[DerivedUnitInstance("PerCubicCentimetre", "[*]", new[] { "CubicCentimetre" })]
[DerivedUnitInstance("PerCubicMillimetre", "[*]", new[] { "CubicMillimetre" })]
[DerivedUnitInstance("PerCubicKilometre", "[*]", new[] { "CubicKilometre" })]
[DerivedUnitInstance("PerCubicInch", "[*]", new[] { "CubicInch" })]
[DerivedUnitInstance("PerCubicFoot", "[*]", new[] { "CubicFoot" })]
[DerivedUnitInstance("PerCubicYard", "[*]", new[] { "CubicYard" })]
[DerivedUnitInstance("PerCubicMile", "[*]", new[] { "CubicMile" })]
[DerivedUnitInstance("PerLitre", "[*]", new[] { "Litre" })]
[DerivedUnitInstance("PerDecilitre", "[*]", new[] { "Decilitre" })]
[DerivedUnitInstance("PerCentilitre", "[*]", new[] { "Centilitre" })]
[DerivedUnitInstance("PerMillilitre", "[*]", new[] { "Millilitre" })]
[DerivableUnit("1 / {0}", typeof(UnitOfVolume))]
[Unit(typeof(VolumetricFrequency))]
public readonly partial record struct UnitOfVolumetricFrequency { }
