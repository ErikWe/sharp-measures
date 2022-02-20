#nullable enable

namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class UnitOfAngularVelocityDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
            yield return new object?[] { UnitOfAngularVelocity.RadianPerSecond };
            yield return new object?[] { UnitOfAngularVelocity.DegreePerSecond };
            yield return new object?[] { UnitOfAngularVelocity.RevolutionPerSecond };
            yield return new object?[] { UnitOfAngularVelocity.RevolutionPerMinute };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
