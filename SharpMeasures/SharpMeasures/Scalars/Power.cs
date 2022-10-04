namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfPower), DefaultUnitInstanceName = "Watt", DefaultUnitInstanceSymbol = "W")]
public readonly partial record struct Power { }

[QuantityOperation(typeof(Speed), typeof(Force), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(Energy), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Energy), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Energy), typeof(Frequency), OperatorType.Division)]
public readonly partial record struct Power { }
