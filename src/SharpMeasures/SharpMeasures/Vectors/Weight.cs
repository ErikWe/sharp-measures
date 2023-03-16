namespace SharpMeasures;

using SharpMeasures.Generators;

[QuantityOperation(typeof(GravitationalAccelerationN), typeof(Mass), OperatorType.Division)]
[SpecializedVectorGroup(typeof(ForceN), Scalar = typeof(Weight))]
public static partial class WeightN { }

[VectorGroupMember(typeof(WeightN))]
public readonly partial record struct Weight2 { }

[VectorGroupMember(typeof(WeightN))]
public readonly partial record struct Weight3 { }

[VectorGroupMember(typeof(WeightN))]
public readonly partial record struct Weight4 { }
