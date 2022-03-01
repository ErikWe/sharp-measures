#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Yank3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfYankDataset>))]
    public void InUnit(Vector3 expected, UnitOfYank unit)
    {
        Yank3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InNewtonPerSecond(Vector3 expected)
    {
        Yank3 quantity = new(expected, UnitOfYank.NewtonPerSecond);

        Vector3 actual = quantity.NewtonsPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
