using System.Collections;
using System.Collections.Generic;

namespace ErikWe.SharpMeasures.Tests.Datasets.Scalar
{
    public class ScalarDataset : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object?[] { double.NaN };
            yield return new object?[] { double.PositiveInfinity };
            yield return new object?[] { double.NegativeInfinity };
            yield return new object?[] { 0 };
            yield return new object?[] { -3372.00237 };
            yield return new object?[] { 812378.3032 };
            yield return new object?[] { null };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
