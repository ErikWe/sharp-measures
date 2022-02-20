#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfEnergyDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfEnergy.Joule };
            yield return new object?[] { UnitOfEnergy.Kilojoule };
            yield return new object?[] { UnitOfEnergy.Megajoule };
            yield return new object?[] { UnitOfEnergy.Gigajoule };
            yield return new object?[] { UnitOfEnergy.KilowattHour };
            yield return new object?[] { UnitOfEnergy.Calorie };
            yield return new object?[] { UnitOfEnergy.Kilocalorie };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
