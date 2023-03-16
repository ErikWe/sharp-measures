namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(AngularMomentum), Vector = typeof(OrbitalAngularMomentumN))]
public readonly partial record struct OrbitalAngularMomentum { }

[QuantityOperation(typeof(SpecificOrbitalAngularMomentum), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(OrbitalAngularSpeed), typeof(MomentOfInertia), OperatorType.Division)]
public readonly partial record struct OrbitalAngularMomentum { }
