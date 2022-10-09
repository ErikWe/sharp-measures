namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(AngularAcceleration), Vector = typeof(OrbitalAngularAccelerationN))]
public readonly partial record struct OrbitalAngularAcceleration { }

[QuantityOperation(typeof(OrbitalAngularSpeed), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(OrbitalAngularSpeed), typeof(Frequency), OperatorType.Division)]
public readonly partial record struct OrbitalAngularAcceleration { }
