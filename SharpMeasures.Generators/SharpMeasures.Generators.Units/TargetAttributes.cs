namespace SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

public static class TargetAttributes
{
    public static IEnumerable<Type> Attributes { get; } = new[] { typeof(UnitAttribute) };
}
