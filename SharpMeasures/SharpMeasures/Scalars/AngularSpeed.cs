namespace SharpMeasures;

using SharpMeasures.Generators;

[ScalarQuantity(typeof(UnitOfAngularSpeed), Vector = typeof(AngularVelocityN), DefaultUnit = "RadianPerSecond", DefaultSymbol = "rad∙s⁻¹")]
public readonly partial record struct AngularSpeed { }

[QuantityOperation(typeof(Time), typeof(AngularAcceleration), OperatorType.Division)]
[QuantityOperation(typeof(Frequency), typeof(AngularFrequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(Frequency), typeof(Angle), OperatorType.Division)]
[QuantityOperation(typeof(AngularMomentum), typeof(MomentOfInertia), OperatorType.Division)]
[QuantityOperation(typeof(AngularAcceleration), typeof(Frequency), OperatorType.Multiplication)]
[QuantityOperation(typeof(AngularAcceleration), typeof(Time), OperatorType.Division)]
[QuantityOperation(typeof(Angle), typeof(Time), OperatorType.Multiplication)]
[QuantityOperation(typeof(Angle), typeof(Frequency), OperatorType.Division)]
public readonly partial record struct AngularSpeed { }
