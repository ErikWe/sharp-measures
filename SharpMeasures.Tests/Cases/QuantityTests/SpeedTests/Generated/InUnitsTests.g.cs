#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVelocityDataset>))]
    public void InUnit(Scalar expected, UnitOfVelocity unit)
    {
        Speed quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMetrePerSecond(Scalar expected)
    {
        Speed quantity = new(expected, UnitOfVelocity.MetrePerSecond);

        Scalar actual = quantity.MetresPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilometrePerSecond(Scalar expected)
    {
        Speed quantity = new(expected, UnitOfVelocity.KilometrePerSecond);

        Scalar actual = quantity.KilometresPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilometrePerHour(Scalar expected)
    {
        Speed quantity = new(expected, UnitOfVelocity.KilometrePerHour);

        Scalar actual = quantity.KilometresPerHour;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFootPerSecond(Scalar expected)
    {
        Speed quantity = new(expected, UnitOfVelocity.FootPerSecond);

        Scalar actual = quantity.FeetPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InYardPerSecond(Scalar expected)
    {
        Speed quantity = new(expected, UnitOfVelocity.YardPerSecond);

        Scalar actual = quantity.YardsPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMilePerHour(Scalar expected)
    {
        Speed quantity = new(expected, UnitOfVelocity.MilePerHour);

        Scalar actual = quantity.MilesPerHour;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
