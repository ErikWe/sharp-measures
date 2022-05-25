namespace SharpMeasures;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System;

[GeneratedScalar(typeof(UnitOfLength))]
public class Length { }

[DerivableUnit("asd", new Type[] { typeof(Length), typeof(Length) })]
[GeneratedUnit(typeof(Length), AllowBias = false)]
public readonly partial record struct UnitOfLength { }