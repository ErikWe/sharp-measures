namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfYank), DefaultUnitInstanceName = "NewtonPerSecond", DefaultUnitInstanceSymbol = "N∙s⁻¹")]
public readonly partial record struct Yank { }

[QuantityOperation(typeof(Frequency), typeof(Force), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Frequency), OperatorType.Division)]
public readonly partial record struct Yank { }
