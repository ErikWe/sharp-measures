namespace SharpMeasures;

using SharpMeasures.Generators.Units;

[DerivedUnitInstance("Newton", "[*]s", new[] { "Kilogram", "MetrePerSecondSquared" })]
[ScaledUnitInstance("PoundForce", "PoundsForce", "Newton", 4.4482216152605)]
[DerivableUnit("{0} * {1}", typeof(UnitOfMass), typeof(UnitOfAcceleration), Permutations = true)]
[SharpMeasuresUnit(typeof(Force))]
public readonly partial record struct UnitOfForce { }
