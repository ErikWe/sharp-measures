#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using System.Collections;
using System.Collections.Generic;

public class SolidAngleDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        IEnumerator<object?[]> scalarEnumerator = new ScalarDataset().GetEnumerator();

        while (scalarEnumerator.MoveNext())
        {
            yield return new object?[] { new Quantities.SolidAngle((Quantities.Scalar?)scalarEnumerator.Current[0] ?? Quantities.Scalar.Zero) };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
