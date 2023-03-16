namespace SharpMeasures;

using SharpMeasures.Generators;

[DerivedUnitInstance("PerRadian", "[*]", new[] { "Radian" })]
[DerivedUnitInstance("PerDegree", "[*]", new[] { "Degree" })]
[DerivedUnitInstance("PerRevolution", "[*]", new[] { "Revolution" })]
[DerivableUnit("1 / {0}", typeof(UnitOfAngle))]
[Unit(typeof(AngularFrequency))]
public readonly partial record struct UnitOfAngularFrequency { }
