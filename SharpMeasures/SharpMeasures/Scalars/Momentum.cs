namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfMomentum), DefaultUnitInstanceName = "KilogramMetrePerSecond", DefaultUnitInstanceSymbol = "kg∙m∙s⁻¹")]
public readonly partial record struct Momentum { }

[QuantityOperation(typeof(Speed), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(Pace), typeof(Energy), OperatorType.Division)]
[QuantityOperation(typeof(Mass), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Energy), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Energy), typeof(Pace), OperatorType.Division)]
public readonly partial record struct Momentum { }
