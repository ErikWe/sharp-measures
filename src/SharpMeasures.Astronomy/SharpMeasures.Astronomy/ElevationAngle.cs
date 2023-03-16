namespace SharpMeasures.Astronomy;

using SharpMeasures.Generators;

[QuantityProcess("AsInclination", typeof(Inclination), "new(90 - Degrees, global::SharpMeasures.UnitOfAngle.Degree)")]
[SpecializedScalarQuantity(typeof(Angle))]
public readonly partial record struct ElevationAngle { }
