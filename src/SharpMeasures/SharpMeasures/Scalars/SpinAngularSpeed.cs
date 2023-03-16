namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(AngularSpeed), Vector = typeof(SpinAngularVelocityN))]
public readonly partial record struct SpinAngularSpeed { }

[QuantityOperation(typeof(SpinAngularAcceleration), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpinAngularAcceleration), typeof(Time), OperatorType.Division)]
public readonly partial record struct SpinAngularSpeed { }
