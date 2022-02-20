#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfAngleDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfAngle.Radian };
            yield return new object?[] { UnitOfAngle.Milliradian };
            yield return new object?[] { UnitOfAngle.Degree };
            yield return new object?[] { UnitOfAngle.Gradian };
            yield return new object?[] { UnitOfAngle.Arcminute };
            yield return new object?[] { UnitOfAngle.Arcsecond };
            yield return new object?[] { UnitOfAngle.Milliarcsecond };
            yield return new object?[] { UnitOfAngle.Microarcsecond };
            yield return new object?[] { UnitOfAngle.Turn };
            yield return new object?[] { UnitOfAngle.HalfTurn };
            yield return new object?[] { UnitOfAngle.QuarterTurn };
            yield return new object?[] { UnitOfAngle.Centiturn };
            yield return new object?[] { UnitOfAngle.Milliturn };
            yield return new object?[] { UnitOfAngle.BinaryDegree };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
