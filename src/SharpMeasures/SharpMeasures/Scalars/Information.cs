﻿namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfInformation), DefaultUnit = "Bit", DefaultSymbol = "b")]
public readonly partial record struct Information { }

[QuantityOperation(typeof(Time), typeof(InformationFlowRate), OperatorType.Division)]
[QuantityOperation(typeof(InformationFlowRate), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(InformationFlowRate), typeof(Time), OperatorType.Division)]
public readonly partial record struct Information { }
