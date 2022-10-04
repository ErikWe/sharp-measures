namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfSolidAngle), DefaultUnitInstanceName = "Steradian", DefaultUnitInstanceSymbol = "sr")]
public readonly partial record struct SolidAngle { }

[QuantityOperation(typeof(Angle), typeof(Angle), OperatorType.Division)]
public readonly partial record struct SolidAngle { }

[QuantityProcess("SquareRoot", typeof(Angle), "new(global::System.Math.Sqrt(Magnitude))")]
public readonly partial record struct SolidAngle { }
