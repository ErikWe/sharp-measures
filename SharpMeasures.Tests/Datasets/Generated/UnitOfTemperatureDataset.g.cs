#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfTemperatureDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfTemperature.Kelvin };
            yield return new object?[] { UnitOfTemperature.Celsius };
            yield return new object?[] { UnitOfTemperature.Rankine };
            yield return new object?[] { UnitOfTemperature.Fahrenheit };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
