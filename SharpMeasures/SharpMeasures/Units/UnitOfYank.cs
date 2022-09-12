namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("NewtonPerSecond", "s[Per]", new[] { "Newton", "Second" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfForce), typeof(UnitOfTime))]
[SharpMeasuresUnit(typeof(Yank))]
public readonly partial record struct UnitOfYank { }
