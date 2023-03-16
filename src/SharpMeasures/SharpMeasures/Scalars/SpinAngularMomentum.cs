namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(AngularMomentum), Vector = typeof(SpinAngularMomentumN))]
public readonly partial record struct SpinAngularMomentum { }

[QuantityOperation(typeof(SpecificSpinAngularMomentum), typeof(Mass), OperatorType.Division)]
[QuantityOperation(typeof(SpinAngularSpeed), typeof(MomentOfInertia), OperatorType.Division)]
public readonly partial record struct SpinAngularMomentum { }
