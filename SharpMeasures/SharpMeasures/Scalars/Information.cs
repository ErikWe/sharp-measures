namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfInformation), DefaultUnitInstanceName = "Bit", DefaultUnitInstanceSymbol = "b")]
public readonly partial record struct Information { }

[DerivedQuantity("{0} / {1}", typeof(InformationFlowRate), typeof(Frequency))]
[DerivedQuantity("{0} * {1}", typeof(InformationFlowRate), typeof(Time), Permutations = true)]
public readonly partial record struct Information { }
