namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAngularMomentum), Vector = typeof(AngularMomentumN), DefaultUnit = "KilogramSquareMetrePerSecond", DefaultSymbol = "kg∙m²∙s⁻¹")]
public readonly partial record struct AngularMomentum { }

[QuantityOperation(typeof(Torque), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Torque), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Time), typeof(Torque), OperatorType.Division)]
[QuantityOperation(typeof(SpecificAngularMomentum), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(MomentOfInertia), typeof(AngularSpeed), OperatorType.Division)]
[QuantityOperation(typeof(Mass), typeof(SpecificAngularMomentum), OperatorType.Division)]
[QuantityOperation(typeof(AngularSpeed), typeof(MomentOfInertia), OperatorType.Division)]
public readonly partial record struct AngularMomentum { }
