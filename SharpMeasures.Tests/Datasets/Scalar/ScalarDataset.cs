using System.Collections;
using System.Collections.Generic;

namespace ErikWe.SharpMeasures.Tests.Datasets.Scalar
{
    public class ScalarDataset : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object?[] { new Quantities.Scalar(double.NaN) };
            yield return new object?[] { new Quantities.Scalar(double.PositiveInfinity) };
            yield return new object?[] { new Quantities.Scalar(double.NegativeInfinity) };
            yield return new object?[] { new Quantities.Scalar(0) };
            yield return new object?[] { new Quantities.Scalar(-3372.00237) };
            yield return new object?[] { new Quantities.Scalar(812378.3032) };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
