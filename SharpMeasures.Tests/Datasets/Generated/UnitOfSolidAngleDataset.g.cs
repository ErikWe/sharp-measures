#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfSolidAngleDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfSolidAngle.Steradian };
            yield return new object?[] { UnitOfSolidAngle.SquareRadian };
            yield return new object?[] { UnitOfSolidAngle.SquareDegree };
            yield return new object?[] { UnitOfSolidAngle.SquareArcminute };
            yield return new object?[] { UnitOfSolidAngle.SquareArcsecond };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
