namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("MetrePerSecondCubed", "MetresPerSecondCubed", new[] { "MetrePerSecondSquared", "Second" })]
[DerivedUnitInstance("FootPerSecondCubed", "FeetPerSecondCubed", new[] { "FootPerSecondSquared", "Second" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfAcceleration), typeof(UnitOfTime))]
[SharpMeasuresUnit(typeof(Jerk))]
public readonly partial record struct UnitOfJerk { }
