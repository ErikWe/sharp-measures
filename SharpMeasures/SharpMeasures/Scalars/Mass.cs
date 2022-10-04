namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfMass), DefaultUnitInstanceName = "Kilogram", DefaultUnitInstanceSymbol = "kg")]
public readonly partial record struct Mass { }

[QuantityOperation(typeof(Volume), typeof(SpecificVolume), OperatorType.Multiplication)]
[QuantityOperation(typeof(Volume), typeof(Density), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(MassFlowRate), OperatorType.Division)]
[QuantityOperation(typeof(Momentum), typeof(Speed), OperatorType.Multiplication)]
[QuantityOperation(typeof(MassFlowRate), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(MassFlowRate), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(LinearDensity), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(LinearDensity), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Length), typeof(LinearDensity), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(Acceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(Density), typeof(VolumetricFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Density), typeof(Volume), OperatorType.Division)]
[QuantityOperation(typeof(ArealDensity), typeof(ArealFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(ArealDensity), typeof(Area), OperatorType.Division)]
[QuantityOperation(typeof(Area), typeof(ArealDensity), OperatorType.Division)]
[QuantityOperation(typeof(AngularMomentum), typeof(SpecificAngularMomentum), OperatorType.Multiplication)]
public readonly partial record struct Mass { }
