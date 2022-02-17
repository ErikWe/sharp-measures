namespace ErikWe.SharpMeasures.Tests.Datasets.Unhandled;

using System.Collections;
using System.Collections.Generic;

public class Unhandled3Dataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        IEnumerator<object?[]> tripleScalarEnumerator = new GenericDataset<UnhandledDataset, UnhandledDataset, UnhandledDataset>().GetEnumerator();

        while (tripleScalarEnumerator.MoveNext())
        {
            Quantities.Unhandled x = (Quantities.Unhandled?)tripleScalarEnumerator.Current[0] ?? Quantities.Unhandled.Zero;
            Quantities.Unhandled y = (Quantities.Unhandled?)tripleScalarEnumerator.Current[1] ?? Quantities.Unhandled.Zero;
            Quantities.Unhandled z = (Quantities.Unhandled?)tripleScalarEnumerator.Current[2] ?? Quantities.Unhandled.Zero;

            yield return new object?[] { new Quantities.Unhandled3(x, y, z) };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}