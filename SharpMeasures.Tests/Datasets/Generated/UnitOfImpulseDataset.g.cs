#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfImpulseDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfImpulse.NewtonSecond };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
