using System.Collections;
using System.Collections.Generic;

namespace ErikWe.SharpMeasures.Tests.Datasets.Scalar
{
    public class Scalar2Dataset : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            IEnumerator<object?[]> doubleScalarEnumerator = new GenericDataset<ScalarDataset, ScalarDataset>().GetEnumerator();

            while (doubleScalarEnumerator.MoveNext())
            {
                Quantities.Scalar x = (Quantities.Scalar?)doubleScalarEnumerator.Current[0] ?? Quantities.Scalar.Zero;
                Quantities.Scalar y = (Quantities.Scalar?)doubleScalarEnumerator.Current[1] ?? Quantities.Scalar.Zero;

                yield return new object?[] { new Quantities.Scalar2(x, y) };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
