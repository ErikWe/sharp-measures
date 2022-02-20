#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfPowerDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfPower.Watt };
            yield return new object?[] { UnitOfPower.Kilowatt };
            yield return new object?[] { UnitOfPower.Megawatt };
            yield return new object?[] { UnitOfPower.Gigawatt };
            yield return new object?[] { UnitOfPower.Terawatt };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
