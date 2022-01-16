using System.Collections;
using System.Collections.Generic;

namespace ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity
{
    public class UnhandledQuantityDataset : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            IEnumerator<object?[]> scalarEnumerator = new Scalar.ScalarDataset().GetEnumerator();

            while (scalarEnumerator.MoveNext())
            {
                yield return new object?[] { new Quantities.Definitions.UnhandledQuantity((Quantities.Scalar?)scalarEnumerator.Current[0] ?? Quantities.Scalar.Zero) };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
