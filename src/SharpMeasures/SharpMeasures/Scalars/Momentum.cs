namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfMomentum), Vector = typeof(MomentumN), Difference = typeof(Impulse), DefaultUnit = "KilogramMetrePerSecond", DefaultSymbol = "kg∙m∙s⁻¹")]
public readonly partial record struct Momentum { }

[QuantityOperation(typeof(Time), typeof(Force), OperatorType.Division)]
[QuantityOperation(typeof(Speed), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(Pace), typeof(Energy), OperatorType.Division)]
[QuantityOperation(typeof(Mass), typeof(Pace), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(Speed), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Energy), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(Energy), typeof(Pace), OperatorType.Division)]
public readonly partial record struct Momentum { }
