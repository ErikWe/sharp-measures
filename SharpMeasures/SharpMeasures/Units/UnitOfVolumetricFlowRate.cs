namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("CubicMetrePerSecond", "s[Per]", new[] { "CubicMetre", "Second" })]
[DerivedUnitInstance("LitrePerSecond", "s[Per]", new[] { "Litre", "Second" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfVolume), typeof(UnitOfTime))]
[SharpMeasuresUnit(typeof(VolumetricFlowRate))]
public readonly partial record struct UnitOfVolumetricFlowRate { }
