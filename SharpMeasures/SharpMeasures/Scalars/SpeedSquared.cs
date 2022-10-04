namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfSpeedSquared), DefaultUnitInstanceName = "SquareMetrePerSecondSquared", DefaultUnitInstanceSymbol = "m²∙s⁻²")]
public readonly partial record struct SpeedSquared { }

[QuantityOperation(typeof(Speed), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(Speed), typeof(Speed), OperatorType.Division)]
public readonly partial record struct SpeedSquared { }

[QuantityProcess("SquareRoot", typeof(Speed), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct SpeedSquared { }
