namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfForce), Vector = typeof(ForceN), DefaultUnit = "Newton", DefaultSymbol = "N")]
public readonly partial record struct Force { }

[QuantityOperation(typeof(Yank), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Yank), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(Yank), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Energy), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Torque), OperatorType.Division)]
[QuantityOperation(typeof(SpatialFrequency), typeof(Work), OperatorType.Division)]
[QuantityOperation(typeof(Pressure), typeof(ArealFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Pressure), typeof(Area), OperatorType.Division)]
[QuantityOperation(typeof(Power), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Power), typeof(Pace), OperatorType.Division)]
[QuantityOperation(typeof(Pace), typeof(Power), OperatorType.Division)]
[QuantityOperation(typeof(Mass), typeof(Acceleration), OperatorType.Division)]
[QuantityOperation(typeof(Impulse), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Impulse), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(Momentum), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(Impulse), OperatorType.Division)]
[QuantityOperation(typeof(Energy), typeof(Length), OperatorType.Multiplication)]
[QuantityOperation(typeof(Energy), typeof(SpatialFrequency), OperatorType.Division)]
[QuantityOperation(typeof(Area), typeof(Pressure), OperatorType.Division)]
[QuantityOperation(typeof(Acceleration), typeof(Mass), OperatorType.Division)]
public readonly partial record struct Force { }
