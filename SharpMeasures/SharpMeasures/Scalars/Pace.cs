namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfPace), DefaultUnitInstanceName = "SecondPerMetre", DefaultUnitInstanceSymbol = "s∙m⁻¹")]
public readonly partial record struct Pace { }

[QuantityOperation(typeof(TimeSquared), typeof(Absement), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Time), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(SpeedSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Scalar), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Momentum), typeof(Energy), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Acceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Power), OperatorType.Multiplication)]
public readonly partial record struct Pace { }
