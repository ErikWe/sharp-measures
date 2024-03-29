﻿namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfJerk), Vector = typeof(JerkN), DefaultUnit = "MetrePerSecondCubed", DefaultSymbol = "m∙s⁻³")]
public readonly partial record struct Jerk { }

[QuantityOperation(typeof(Speed), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(FrequencyDrift), OperatorType.Division)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(FrequencyDrift), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(Acceleration), OperatorType.Division)]
[QuantityOperation(typeof(Acceleration), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Frequency), OperatorType.Division)]
public readonly partial record struct Jerk { }
