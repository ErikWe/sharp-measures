#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Momentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfMomentumDataset>))]
    public void InUnit(Vector3 expected, UnitOfMomentum unit)
    {
        Momentum3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InKilogramMetrePerSecond(Vector3 expected)
    {
        Momentum3 quantity = new(expected, UnitOfMomentum.KilogramMetrePerSecond);

        Vector3 actual = quantity.KilogramMetresPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
