#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfFrequencyDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfFrequency.PerSecond };
            yield return new object?[] { UnitOfFrequency.PerMinute };
            yield return new object?[] { UnitOfFrequency.PerHour };
            yield return new object?[] { UnitOfFrequency.Hertz };
            yield return new object?[] { UnitOfFrequency.Kilohertz };
            yield return new object?[] { UnitOfFrequency.Megahertz };
            yield return new object?[] { UnitOfFrequency.Gigahertz };
            yield return new object?[] { UnitOfFrequency.Terahertz };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
