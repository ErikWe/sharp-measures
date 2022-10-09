namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedScalarQuantity(typeof(SpecificAngularMomentum), Vector = typeof(SpecificSpinAngularMomentumN))]
public readonly partial record struct SpecificSpinAngularMomentum { }

[QuantityOperation(typeof(SpinAngularMomentum), typeof(Mass), OperatorType.Multiplication)]
public readonly partial record struct SpecificSpinAngularMomentum { }
