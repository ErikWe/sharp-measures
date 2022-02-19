namespace ErikWe.SharpMeasures.Tests.Datasets;

using System.Collections;
using System.Collections.Generic;

public class Vector3Dataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        IEnumerator<object?[]> tripleScalarEnumerator = new GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>().GetEnumerator();

        while (tripleScalarEnumerator.MoveNext())
        {
            Quantities.Scalar x = (Quantities.Scalar?)tripleScalarEnumerator.Current[0] ?? Quantities.Scalar.Zero;
            Quantities.Scalar y = (Quantities.Scalar?)tripleScalarEnumerator.Current[1] ?? Quantities.Scalar.Zero;
            Quantities.Scalar z = (Quantities.Scalar?)tripleScalarEnumerator.Current[2] ?? Quantities.Scalar.Zero;

            yield return new object?[] { new Quantities.Vector3(x, y, z) };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}