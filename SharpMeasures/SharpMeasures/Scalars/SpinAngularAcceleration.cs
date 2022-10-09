namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(AngularAcceleration), Vector = typeof(SpinAngularAccelerationN))]
public readonly partial record struct SpinAngularAcceleration { }

[QuantityOperation(typeof(SpinAngularSpeed), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpinAngularSpeed), typeof(Frequency), OperatorType.Division)]
public readonly partial record struct SpinAngularAcceleration { }
