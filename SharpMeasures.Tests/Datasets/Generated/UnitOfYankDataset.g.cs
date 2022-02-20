#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfYankDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfYank.NewtonPerSecond };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
