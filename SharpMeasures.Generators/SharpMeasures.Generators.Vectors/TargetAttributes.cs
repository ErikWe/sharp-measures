namespace SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;

public static class TargetAttributes
{
    public static IEnumerable<Type> Attributes { get; } = new[] { typeof(VectorQuantityAttribute), typeof(SpecializedVectorQuantityAttribute), typeof(VectorGroupAttribute), typeof(SpecializedVectorGroupAttribute), typeof(VectorGroupMemberAttribute) };
}
