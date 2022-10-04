namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAngularAcceleration), DefaultUnitInstanceName = "RadianPerSecondSquared", DefaultUnitInstanceSymbol = "rad∙s⁻²")]
public readonly partial record struct AngularAcceleration { }

[QuantityOperation(typeof(FrequencyDrift), typeof(Angle), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(AngularSpeed), OperatorType.Division)]
[QuantityOperation(typeof(AngularSpeed), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularSpeed), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(Angle), typeof(TimeSquared), OperatorType.Multiplication)]
[QuantityOperation(typeof(Angle), typeof(FrequencyDrift), OperatorType.Division)]
public readonly partial record struct AngularAcceleration { }
