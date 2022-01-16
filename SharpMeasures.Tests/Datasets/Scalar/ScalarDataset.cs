using System.Collections;
using System.Collections.Generic;

namespace ErikWe.SharpMeasures.Tests.Datasets.Scalar
{
    public class ScalarDataset : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            IEnumerator<object?[]> doubleEnumerator = new Double.DoubleDataset().GetEnumerator();

            while (doubleEnumerator.MoveNext())
            {
                yield return new object?[] { new Quantities.Scalar((double)(doubleEnumerator.Current[0] ?? 0)) };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
