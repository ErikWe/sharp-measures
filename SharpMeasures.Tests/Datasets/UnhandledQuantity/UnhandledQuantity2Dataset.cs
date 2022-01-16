using System.Collections;
using System.Collections.Generic;

namespace ErikWe.SharpMeasures.Tests.Datasets.UnhandledQuantity
{
    public class UnhandledQuantity2Dataset : IEnumerable<object?[]>
    {
        public IEnumerator<object?[]> GetEnumerator()
        {
            IEnumerator<object?[]> doubleScalarEnumerator = new GenericDataset<UnhandledQuantityDataset, UnhandledQuantityDataset>().GetEnumerator();

            while (doubleScalarEnumerator.MoveNext())
            {
                Quantities.Definitions.UnhandledQuantity x
                    = (Quantities.Definitions.UnhandledQuantity?)doubleScalarEnumerator.Current[0] ?? Quantities.Definitions.UnhandledQuantity.Zero;
                Quantities.Definitions.UnhandledQuantity y
                    = (Quantities.Definitions.UnhandledQuantity?)doubleScalarEnumerator.Current[1] ?? Quantities.Definitions.UnhandledQuantity.Zero;

                yield return new object?[] { new Quantities.Definitions.UnhandledQuantity2(x, y) };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
