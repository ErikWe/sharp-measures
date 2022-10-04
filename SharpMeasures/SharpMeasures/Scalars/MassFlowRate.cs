namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfMassFlowRate))]
public readonly partial record struct MassFlowRate { }

[QuantityOperation(typeof(Mass), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Mass), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(Mass), OperatorType.Division)]
public readonly partial record struct MassFlowRate { }
