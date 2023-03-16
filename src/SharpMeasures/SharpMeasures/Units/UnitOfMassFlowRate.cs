namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("KilogramPerSecond", "s[Per]", new[] { "Kilogram", "Second" })]
[DerivedUnitInstance("PoundPerSecond", "s[Per]", new[] { "Pound", "Second" })]
[DerivableUnit("{0} / {1}", typeof(UnitOfMass), typeof(UnitOfTime))]
[Unit(typeof(MassFlowRate))]
public readonly partial record struct UnitOfMassFlowRate { }
