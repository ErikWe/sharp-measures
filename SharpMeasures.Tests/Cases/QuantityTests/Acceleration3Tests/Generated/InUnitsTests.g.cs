#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Acceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAccelerationDataset>))]
    public void InUnit(Vector3 expected, UnitOfAcceleration unit)
    {
        Acceleration3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void StandardGravityMultiples(Vector3 expected)
    {
        Acceleration3 quantity = new(expected, UnitOfAcceleration.StandardGravity);

        Vector3 actual = quantity.StandardGravityMultiples;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMetrePerSecondSquared(Vector3 expected)
    {
        Acceleration3 quantity = new(expected, UnitOfAcceleration.MetrePerSecondSquared);

        Vector3 actual = quantity.MetresPerSecondSquared;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InFootPerSecondSquared(Vector3 expected)
    {
        Acceleration3 quantity = new(expected, UnitOfAcceleration.FootPerSecondSquared);

        Vector3 actual = quantity.FootsPerSecondSquared;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InKilometrePerHourPerSecond(Vector3 expected)
    {
        Acceleration3 quantity = new(expected, UnitOfAcceleration.KilometrePerHourPerSecond);

        Vector3 actual = quantity.KilometresPerHourPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMilePerHourPerSecond(Vector3 expected)
    {
        Acceleration3 quantity = new(expected, UnitOfAcceleration.MilePerHourPerSecond);

        Vector3 actual = quantity.MilesPerHourPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
