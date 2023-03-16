namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfSpeedSquared), DefaultUnit = "SquareMetrePerSecondSquared", DefaultSymbol = "m²∙s⁻²")]
public readonly partial record struct SpeedSquared { }

[QuantityOperation(typeof(Speed), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Distance), typeof(Acceleration), OperatorType.Division)]
[QuantityOperation(typeof(Acceleration), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Acceleration), typeof(Length), OperatorType.Division)]
public readonly partial record struct SpeedSquared { }

[QuantityProcess("SquareRoot", typeof(Speed), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct SpeedSquared { }
