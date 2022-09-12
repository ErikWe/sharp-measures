namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("MetreSecond", "[*]s", new[] { "Metre", "Second" })]
[DerivableUnit("{0} * {1}", typeof(UnitOfLength), typeof(UnitOfTime), Permutations = true)]
[SharpMeasuresUnit(typeof(Absement))]
public readonly partial record struct UnitOfAbsement { }
