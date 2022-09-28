namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfMassFlowRate))]
public readonly partial record struct MassFlowRate { }

[DerivedQuantity("{0} * {1}", typeof(Mass), typeof(Frequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Mass), typeof(Time))]
public readonly partial record struct MassFlowRate { }
