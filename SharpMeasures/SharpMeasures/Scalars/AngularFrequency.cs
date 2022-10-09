namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAngularFrequency), DefaultUnit = "PerRadian", DefaultSymbol = "rad⁻¹")]
public readonly partial record struct AngularFrequency { }

[QuantityOperation(typeof(Scalar), typeof(Angle), OperatorType.Multiplication)]
[QuantityOperation(typeof(FrequencyDrift), typeof(AngularAcceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(AngularSpeed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Angle), typeof(SolidAngle), OperatorType.Multiplication)]
[QuantityOperation(typeof(Angle), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
public readonly partial record struct AngularFrequency { }
