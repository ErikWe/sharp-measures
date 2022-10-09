namespace SharpMeasures;

using SharpMeasures.Generators;

[VectorGroup(typeof(UnitOfSpecificAngularMomentum), Scalar = typeof(SpecificAngularMomentum), DefaultUnit = "SquareMetrePerSecond", DefaultSymbol = "m²∙s⁻¹")]
public static partial class SpecificAngularMomentumN { }

[VectorGroupMember(typeof(SpecificAngularMomentumN))]
public readonly partial record struct SpecificAngularMomentum2 { }

[VectorGroupMember(typeof(SpecificAngularMomentumN))]
public readonly partial record struct SpecificAngularMomentum3 { }

[VectorGroupMember(typeof(SpecificAngularMomentumN))]
public readonly partial record struct SpecificAngularMomentum4 { }
