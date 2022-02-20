#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfPressureDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfPressure.Pascal };
            yield return new object?[] { UnitOfPressure.Kilopascal };
            yield return new object?[] { UnitOfPressure.Bar };
            yield return new object?[] { UnitOfPressure.StandardAtmosphere };
            yield return new object?[] { UnitOfPressure.PoundForcePerSquareInch };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
