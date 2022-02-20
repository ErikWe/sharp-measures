#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfVolumeDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfVolume.CubicMetre };
            yield return new object?[] { UnitOfVolume.CubicDecimetre };
            yield return new object?[] { UnitOfVolume.Litre };
            yield return new object?[] { UnitOfVolume.Millilitre };
            yield return new object?[] { UnitOfVolume.Centilitre };
            yield return new object?[] { UnitOfVolume.Decilitre };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
