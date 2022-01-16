using System.Collections;
using System.Collections.Generic;

namespace ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity
{
    public class UnhandledQuantity3Dataset : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            IEnumerator<object?[]> tripleScalarEnumerator = new GenericDataset<UnhandledQuantityDataset, UnhandledQuantityDataset, UnhandledQuantityDataset>().GetEnumerator();

            while (tripleScalarEnumerator.MoveNext())
            {
                Quantities.Definitions.UnhandledQuantity x
                    = (Quantities.Definitions.UnhandledQuantity?)tripleScalarEnumerator.Current[0] ?? Quantities.Definitions.UnhandledQuantity.Zero;
                Quantities.Definitions.UnhandledQuantity y
                    = (Quantities.Definitions.UnhandledQuantity?)tripleScalarEnumerator.Current[1] ?? Quantities.Definitions.UnhandledQuantity.Zero;
                Quantities.Definitions.UnhandledQuantity z
                    = (Quantities.Definitions.UnhandledQuantity?)tripleScalarEnumerator.Current[2] ?? Quantities.Definitions.UnhandledQuantity.Zero;

                yield return new object?[] { new Quantities.Definitions.UnhandledQuantity3(x, y, z) };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
