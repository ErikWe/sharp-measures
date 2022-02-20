#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfDensityDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfDensity.KilogramPerCubicMetre };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
