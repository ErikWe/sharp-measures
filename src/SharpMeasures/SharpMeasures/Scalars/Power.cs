namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfPower), DefaultUnit = "Watt", DefaultSymbol = "W")]
public readonly partial record struct Power { }

[QuantityOperation(typeof(Work), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Work), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(Force), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(Energy), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(Work), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Speed), OperatorType.Division)]
public readonly partial record struct Power { }
