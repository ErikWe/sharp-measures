#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfTimeDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfTime.Second };
            yield return new object?[] { UnitOfTime.Minute };
            yield return new object?[] { UnitOfTime.Hour };
            yield return new object?[] { UnitOfTime.Day };
            yield return new object?[] { UnitOfTime.Week };
            yield return new object?[] { UnitOfTime.CommonYear };
            yield return new object?[] { UnitOfTime.JulianYear };
            yield return new object?[] { UnitOfTime.Femtosecond };
            yield return new object?[] { UnitOfTime.Picosecond };
            yield return new object?[] { UnitOfTime.Nanosecond };
            yield return new object?[] { UnitOfTime.Microsecond };
            yield return new object?[] { UnitOfTime.Millisecond };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
