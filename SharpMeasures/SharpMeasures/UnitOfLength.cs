namespace SharpMeasures;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System;

[DimensionalEquivalence(typeof(string), typeof(int), ImplicitCastOperator = false)]
[GeneratedScalar(typeof(UnitOfLength))]
public class Length { }

[DerivableUnit("asd", new Type[] { typeof(Length), typeof(Length) })]
[GeneratedUnit(typeof(Length), SupportsBiasedQuantities = false)]
public readonly partial record struct UnitOfLength { }