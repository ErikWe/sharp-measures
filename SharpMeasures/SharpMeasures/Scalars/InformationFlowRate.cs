namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfInformationFlowRate), DefaultUnitInstanceName = "BitPerSecond", DefaultUnitInstanceSymbol = "b∙s⁻¹")]
public readonly partial record struct InformationFlowRate { }

[QuantityOperation(typeof(Information), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(Information), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Information), OperatorType.Division)]
public readonly partial record struct InformationFlowRate { }
