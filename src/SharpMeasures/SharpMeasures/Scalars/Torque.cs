namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfTorque), Vector = typeof(TorqueN), DefaultUnit = "NewtonMetre", DefaultSymbol = "N∙m")]
public readonly partial record struct Torque { }

[QuantityOperation(typeof(MomentOfInertia), typeof(AngularAcceleration), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(AngularMomentum), OperatorType.Division)]
[QuantityOperation(typeof(Force), typeof(SpatialFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Force), typeof(Length), OperatorType.Division)]
[QuantityOperation(typeof(Distance), typeof(Force), OperatorType.Division)]
[QuantityOperation(typeof(AngularMomentum), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularMomentum), typeof(Frequency), OperatorType.Division)]
[QuantityOperation(typeof(AngularAcceleration), typeof(MomentOfInertia), OperatorType.Division)]
public readonly partial record struct Torque { }
