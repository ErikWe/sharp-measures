namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("RadianPerSecondSquared", "s[Per]", new[] { "RadianPerSecond", "Second" })]
[DerivedUnitInstance("DegreePerSecondSquared", "s[Per]", new[] { "DegreePerSecond", "Second" })]
[DerivedUnitInstance("RevolutionPerSecondSquared", "s[Per]", new[] { "RevolutionPerSecond", "Second" })]
[DerivedUnitInstance("RevolutionPerMinutePerSecond", "RevolutionsPerMinutePerSecond", new[] { "RevolutionPerMinute", "Second" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfAngularSpeed), typeof(UnitOfTime))]
[Unit(typeof(AngularAcceleration))]
public readonly partial record struct UnitOfAngularAcceleration { }
