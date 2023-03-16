namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfMomentOfInertia), DefaultUnit = "KilogramSquareMetre", DefaultSymbol = "kg∙m²")]
public readonly partial record struct MomentOfInertia { }

[QuantityOperation(typeof(Torque), typeof(AngularAcceleration), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularMomentum), typeof(AngularSpeed), OperatorType.Multiplication)]
public readonly partial record struct MomentOfInertia { }

[QuantityOperation(typeof(TorqueN), typeof(AngularAccelerationN), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularMomentumN), typeof(AngularVelocityN), OperatorType.Multiplication)]
public readonly partial record struct MomentOfInertia { }
