#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfLengthDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfLength.Metre };
            yield return new object?[] { UnitOfLength.Femtometre };
            yield return new object?[] { UnitOfLength.Picometre };
            yield return new object?[] { UnitOfLength.Nanometre };
            yield return new object?[] { UnitOfLength.Micrometre };
            yield return new object?[] { UnitOfLength.Millimetre };
            yield return new object?[] { UnitOfLength.Centimetre };
            yield return new object?[] { UnitOfLength.Decimetre };
            yield return new object?[] { UnitOfLength.Kilometre };
            yield return new object?[] { UnitOfLength.AstronomicalUnit };
            yield return new object?[] { UnitOfLength.LightYear };
            yield return new object?[] { UnitOfLength.Parsec };
            yield return new object?[] { UnitOfLength.Kiloparsec };
            yield return new object?[] { UnitOfLength.Megaparsec };
            yield return new object?[] { UnitOfLength.Gigaparsec };
            yield return new object?[] { UnitOfLength.Inch };
            yield return new object?[] { UnitOfLength.Foot };
            yield return new object?[] { UnitOfLength.Yard };
            yield return new object?[] { UnitOfLength.Mile };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
