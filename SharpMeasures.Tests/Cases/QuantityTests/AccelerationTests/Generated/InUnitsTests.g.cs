#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AccelerationTests;

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
        Acceleration quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InStandardGravity(Scalar expected)
    {
        Acceleration quantity = new(expected, UnitOfAcceleration.StandardGravity);

        Scalar actual = quantity.StandardGravityMultiples;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMetrePerSecondSquared(Scalar expected)
    {
        Acceleration quantity = new(expected, UnitOfAcceleration.MetrePerSecondSquared);

        Scalar actual = quantity.MetresPerSecondSquared;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFootPerSecondSquared(Scalar expected)
    {
        Acceleration quantity = new(expected, UnitOfAcceleration.FootPerSecondSquared);

        Scalar actual = quantity.FootsPerSecondSquared;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilometrePerHourPerSecond(Scalar expected)
    {
        Acceleration quantity = new(expected, UnitOfAcceleration.KilometrePerHourPerSecond);

        Scalar actual = quantity.KilometresPerHourPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMilePerHourPerSecond(Scalar expected)
    {
        Acceleration quantity = new(expected, UnitOfAcceleration.MilePerHourPerSecond);

        Scalar actual = quantity.MilesPerHourPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
