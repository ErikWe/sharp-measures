namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("MetreSecond", "[*]s", new[] { "Metre", "Second" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfLength), typeof(UnitOfTime), Permutations = true)]
[Unit(typeof(Absement))]
public readonly partial record struct UnitOfAbsement { }
