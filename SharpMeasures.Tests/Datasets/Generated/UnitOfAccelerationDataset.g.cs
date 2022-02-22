#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfAccelerationDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfAcceleration.MetrePerSecondSquared };
            yield return new object?[] { UnitOfAcceleration.FootPerSecondSquared };
            yield return new object?[] { UnitOfAcceleration.KilometrePerHourPerSecond };
            yield return new object?[] { UnitOfAcceleration.MilePerHourPerSecond };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
