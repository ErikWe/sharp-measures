namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfMomentum), Vector = typeof(ImpulseN), DefaultUnit = "NewtonSecond", DefaultSymbol = "N∙s")]
public readonly partial record struct Impulse { }

[QuantityOperation(typeof(Time), typeof(Force), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Time), OperatorType.Division)]
public readonly partial record struct Impulse { }
