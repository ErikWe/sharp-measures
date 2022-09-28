namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfAngularAcceleration), DefaultUnitInstanceName = "RadianPerSecondSquared", DefaultUnitInstanceSymbol = "rad∙s⁻²")]
public readonly partial record struct AngularAcceleration { }

[DerivedQuantity("{0} * {1}", typeof(Angle), typeof(FrequencyDrift), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Angle), typeof(TimeSquared))]
[DerivedQuantity("{0} * {1}", typeof(AngularSpeed), typeof(Frequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(AngularSpeed), typeof(Time))]
public readonly partial record struct AngularAcceleration { }
