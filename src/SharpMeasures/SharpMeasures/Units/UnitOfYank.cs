namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("NewtonPerSecond", "s[Per]", new[] { "Newton", "Second" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfForce), typeof(UnitOfTime))]
[Unit(typeof(Yank))]
public readonly partial record struct UnitOfYank { }
