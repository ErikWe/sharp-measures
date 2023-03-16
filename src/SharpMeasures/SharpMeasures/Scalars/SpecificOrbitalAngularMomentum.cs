namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(SpecificAngularMomentum), Vector = typeof(SpecificOrbitalAngularMomentumN))]
public readonly partial record struct SpecificOrbitalAngularMomentum { }

[QuantityOperation(typeof(OrbitalAngularMomentum), typeof(Mass), OperatorType.Multiplication)]
public readonly partial record struct SpecificOrbitalAngularMomentum { }
