#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfAreaDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfArea.SquareMetre };
            yield return new object?[] { UnitOfArea.SquareKilometre };
            yield return new object?[] { UnitOfArea.SquareInch };
            yield return new object?[] { UnitOfArea.SquareMile };
            yield return new object?[] { UnitOfArea.Are };
            yield return new object?[] { UnitOfArea.Hectare };
            yield return new object?[] { UnitOfArea.Acre };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
