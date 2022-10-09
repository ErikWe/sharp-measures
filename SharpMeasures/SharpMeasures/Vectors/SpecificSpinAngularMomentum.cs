namespace SharpMeasures;

using SharpMeasures.Generators;

[SpecializedVectorGroup(typeof(SpecificAngularMomentumN), Scalar = typeof(SpecificSpinAngularMomentum))]
public static partial class SpecificSpinAngularMomentumN { }

[QuantityOperation(typeof(AngularMomentumN), typeof(Mass), OperatorType.Multiplication)]
public static partial class SpecificSpinAngularMomentumN { }

[VectorGroupMember(typeof(SpecificSpinAngularMomentumN))]
public readonly partial record struct SpecificSpinAngularMomentum2 { }

[VectorGroupMember(typeof(SpecificSpinAngularMomentumN))]
public readonly partial record struct SpecificSpinAngularMomentum3 { }

[VectorGroupMember(typeof(SpecificSpinAngularMomentumN))]
public readonly partial record struct SpecificSpinAngularMomentum4 { }
