namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("RadianPerSecond", "s[Per]", new[] { "Radian", "Second" })]
[DerivedUnitInstance("DegreePerSecond", "s[Per]", new[] { "Degree", "Second" })]
[DerivedUnitInstance("RevolutionPerSecond", "s[Per]", new[] { "Revolution", "Second" })]
[DerivedUnitInstance("RevolutionPerMinute", "s[Per]", new[] { "Revolution", "Minute" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfAngle), typeof(UnitOfTime))]
[Unit(typeof(AngularSpeed))]
public readonly partial record struct UnitOfAngularSpeed { }
