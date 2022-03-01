#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAccelerationDataset>))]
    public void InUnit(Scalar expected, UnitOfAcceleration unit)
    {
        GravitationalAcceleration quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InStandardGravity(Scalar expected)
    {
        GravitationalAcceleration quantity = new(expected, UnitOfAcceleration.StandardGravity);

        Scalar actual = quantity.StandardGravityMultiples;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMetrePerSecondSquared(Scalar expected)
    {
        GravitationalAcceleration quantity = new(expected, UnitOfAcceleration.MetrePerSecondSquared);

        Scalar actual = quantity.MetresPerSecondSquared;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFootPerSecondSquared(Scalar expected)
    {
        GravitationalAcceleration quantity = new(expected, UnitOfAcceleration.FootPerSecondSquared);

        Scalar actual = quantity.FootsPerSecondSquared;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilometrePerHourPerSecond(Scalar expected)
    {
        GravitationalAcceleration quantity = new(expected, UnitOfAcceleration.KilometrePerHourPerSecond);

        Scalar actual = quantity.KilometresPerHourPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMilePerHourPerSecond(Scalar expected)
    {
        GravitationalAcceleration quantity = new(expected, UnitOfAcceleration.MilePerHourPerSecond);

        Scalar actual = quantity.MilesPerHourPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
