namespace SharpMeasures.Astronomy;

using SharpMeasures.Generators;

[QuantityProcess("AsElevation", typeof(ElevationAngle), "new(90 - Degrees, global::SharpMeasures.UnitOfAngle.Degree)")]
[SpecializedScalarQuantity(typeof(Angle))]
public readonly partial record struct Inclination { }
