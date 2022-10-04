namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("MetrePerSecondCubed", "MetresPerSecondCubed", new[] { "MetrePerSecondSquared", "Second" })]
[DerivedUnitInstance("FootPerSecondCubed", "FeetPerSecondCubed", new[] { "FootPerSecondSquared", "Second" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfAcceleration), typeof(UnitOfTime))]
[Unit(typeof(Jerk))]
public readonly partial record struct UnitOfJerk { }
