#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfVelocityDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfVelocity.MetrePerSecond };
            yield return new object?[] { UnitOfVelocity.KilometrePerSecond };
            yield return new object?[] { UnitOfVelocity.KilometrePerHour };
            yield return new object?[] { UnitOfVelocity.FootPerSecond };
            yield return new object?[] { UnitOfVelocity.YardPerSecond };
            yield return new object?[] { UnitOfVelocity.MilePerHour };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
