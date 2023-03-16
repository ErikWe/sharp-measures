namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfPressure), DefaultUnit = "Pascal", DefaultSymbol = "Pa")]
public readonly partial record struct Pressure { }

[QuantityOperation(typeof(Force), typeof(Area), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(ArealFrequency), OperatorType.Division)]
[QuantityOperation(typeof(ArealFrequency), typeof(Force), OperatorType.Division)]
public readonly partial record struct Pressure { }
