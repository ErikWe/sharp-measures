namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(AngularSpeed), Vector = typeof(OrbitalAngularVelocityN))]
public readonly partial record struct OrbitalAngularSpeed { }

[QuantityOperation(typeof(OrbitalAngularAcceleration), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(OrbitalAngularAcceleration), typeof(Time), OperatorType.Division)]
public readonly partial record struct OrbitalAngularSpeed { }
