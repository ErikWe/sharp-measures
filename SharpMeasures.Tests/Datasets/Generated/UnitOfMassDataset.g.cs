#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfMassDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfMass.Gram };
            yield return new object?[] { UnitOfMass.Milligram };
            yield return new object?[] { UnitOfMass.Hectogram };
            yield return new object?[] { UnitOfMass.Kilogram };
            yield return new object?[] { UnitOfMass.Tonne };
            yield return new object?[] { UnitOfMass.Ounce };
            yield return new object?[] { UnitOfMass.Pound };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
