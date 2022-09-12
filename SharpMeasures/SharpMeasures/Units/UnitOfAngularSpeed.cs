namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("RadianPerSecond", "s[Per]", new[] { "Radian", "Second" })]
[DerivedUnitInstance("DegreePerSecond", "s[Per]", new[] { "Degree", "Second" })]
[DerivedUnitInstance("RevolutionPerSecond", "s[Per]", new[] { "Turn", "Second" })]
[DerivedUnitInstance("RevolutionPerMinute", "s[Per]", new[] { "Turn", "Minute" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfAngle), typeof(UnitOfTime))]
[SharpMeasuresUnit(typeof(AngularSpeed))]
public readonly partial record struct UnitOfAngularSpeed { }
