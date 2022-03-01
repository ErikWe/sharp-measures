#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using System.Collections;
using System.Collections.Generic;

public class GravitationalAcceleration3Dataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        IEnumerator<object?[]> tripleScalarDataset = new GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>().GetEnumerator();

        while (tripleScalarDataset.MoveNext())
        {
            Quantities.Scalar x = (Quantities.Scalar?)tripleScalarDataset.Current[0] ?? Quantities.Scalar.Zero;
            Quantities.Scalar y = (Quantities.Scalar?)tripleScalarDataset.Current[1] ?? Quantities.Scalar.Zero;
            Quantities.Scalar z = (Quantities.Scalar?)tripleScalarDataset.Current[2] ?? Quantities.Scalar.Zero;

            yield return new object?[] { new Quantities.GravitationalAcceleration3(x, y, z) };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
