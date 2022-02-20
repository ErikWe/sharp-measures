#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfTemperatureGradientDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfTemperatureGradient.KelvinPerMetre };
            yield return new object?[] { UnitOfTemperatureGradient.CelsiusPerMetre };
            yield return new object?[] { UnitOfTemperatureGradient.RankinePerMetre };
            yield return new object?[] { UnitOfTemperatureGradient.FahrenheitPerMetre };
            yield return new object?[] { UnitOfTemperatureGradient.FahrenheitPerFoot };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
