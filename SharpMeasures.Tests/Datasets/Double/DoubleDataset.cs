namespace ErikWe.SharpMeasures.Tests.Datasets.Double;

using System.Collections;
using System.Collections.Generic;

public class DoubleDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        yield return new object?[] { double.NaN };
        yield return new object?[] { double.PositiveInfinity };
        yield return new object?[] { double.NegativeInfinity };
        yield return new object?[] { double.MaxValue };
        yield return new object?[] { double.MinValue };
        yield return new object?[] { double.Epsilon };
        yield return new object?[] { 0d };
        yield return new object?[] { 1d };
        yield return new object?[] { -1d };
        yield return new object?[] { -3372.00237d };
        yield return new object?[] { 812378.3032d };
        yield return new object?[] { 7d };
        yield return new object?[] { -11d };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}