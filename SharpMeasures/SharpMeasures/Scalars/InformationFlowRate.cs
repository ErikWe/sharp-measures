namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

[SharpMeasuresScalar(typeof(UnitOfInformationFlowRate), DefaultUnitInstanceName = "BitPerSecond", DefaultUnitInstanceSymbol = "b∙s⁻¹")]
public readonly partial record struct InformationFlowRate { }

[DerivedQuantity("{0} * {1}", typeof(Information), typeof(Frequency), Permutations = true)]
[DerivedQuantity("{0} / {1}", typeof(Information), typeof(Time))]
public readonly partial record struct InformationFlowRate { }
