namespace SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public static class TargetAttributes
{
    public static IEnumerable<Type> Attributes { get; } = new[] { typeof(ScalarQuantityAttribute), typeof(SpecializedScalarQuantityAttribute) };
}
