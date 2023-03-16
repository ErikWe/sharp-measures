namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfYank), Vector = typeof(YankN), DefaultUnit = "NewtonPerSecond", DefaultSymbol = "N∙s⁻¹")]
public readonly partial record struct Yank { }

[QuantityOperation(typeof(Frequency), typeof(Force), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Frequency), OperatorType.Division)]
public readonly partial record struct Yank { }
