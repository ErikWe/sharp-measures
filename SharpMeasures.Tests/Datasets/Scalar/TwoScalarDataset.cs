using System.Collections;
using System.Collections.Generic;

namespace ErikWe.SharpMeasures.Tests.Datasets.Scalar
{
    public class TwoScalarDataset : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            yield return new object?[] { new Quantities.Scalar(double.NaN), new Quantities.Scalar(30.51) };
            yield return new object?[] { new Quantities.Scalar(double.PositiveInfinity), new Quantities.Scalar(-30.005) };
            yield return new object?[] { new Quantities.Scalar(double.NegativeInfinity), new Quantities.Scalar(-0.765) };
            yield return new object?[] { new Quantities.Scalar(0), new Quantities.Scalar(33) };
            yield return new object?[] { new Quantities.Scalar(-3372.00237), new Quantities.Scalar(976.2) };
            yield return new object?[] { new Quantities.Scalar(812378.3032), new Quantities.Scalar(99.0) };
            yield return new object?[] { new Quantities.Scalar(-89.77), new Quantities.Scalar(-38712329.1) };
            yield return new object?[] { new Quantities.Scalar(double.NaN), new Quantities.Scalar(0.0007) };
            yield return new object?[] { new Quantities.Scalar(double.PositiveInfinity), new Quantities.Scalar(-976.001) };
            yield return new object?[] { new Quantities.Scalar(double.NegativeInfinity), new Quantities.Scalar(0) };
            yield return new object?[] { new Quantities.Scalar(0), new Quantities.Scalar(0) };
            yield return new object?[] { new Quantities.Scalar(double.NaN), new Quantities.Scalar(double.NaN) };
            yield return new object?[] { new Quantities.Scalar(37.65), new Quantities.Scalar(37.65) };
            yield return new object?[] { new Quantities.Scalar(-897.2), new Quantities.Scalar(-897.2) };

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
