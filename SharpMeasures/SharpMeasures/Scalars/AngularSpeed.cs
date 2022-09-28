namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfAngularSpeed), DefaultUnitInstanceName = "RadianPerSecond", DefaultUnitInstanceSymbol = "rad∙s⁻¹")]
public readonly partial record struct AngularSpeed { }

[DerivedQuantity("{0} / {1}", typeof(AngularMomentum), typeof(MomentOfInertia))]
[DerivedQuantity("{0} / {1}", typeof(AngularAcceleration), typeof(Frequency))]
[DerivedQuantity("{0} * {1}", typeof(AngularAcceleration), typeof(Time), Permutations = true)]
[DerivedQuantity("{0} * {1}", typeof(Angle), typeof(Frequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Angle), typeof(Time))]
public readonly partial record struct AngularSpeed { }
