#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Velocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfVelocityDataset>))]
    public void InUnit(Vector3 expected, UnitOfVelocity unit)
    {
        Velocity3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMetrePerSecond(Vector3 expected)
    {
        Velocity3 quantity = new(expected, UnitOfVelocity.MetrePerSecond);

        Vector3 actual = quantity.MetresPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InKilometrePerSecond(Vector3 expected)
    {
        Velocity3 quantity = new(expected, UnitOfVelocity.KilometrePerSecond);

        Vector3 actual = quantity.KilometresPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InKilometrePerHour(Vector3 expected)
    {
        Velocity3 quantity = new(expected, UnitOfVelocity.KilometrePerHour);

        Vector3 actual = quantity.KilometresPerHour;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InFootPerSecond(Vector3 expected)
    {
        Velocity3 quantity = new(expected, UnitOfVelocity.FootPerSecond);

        Vector3 actual = quantity.FeetPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InYardPerSecond(Vector3 expected)
    {
        Velocity3 quantity = new(expected, UnitOfVelocity.YardPerSecond);

        Vector3 actual = quantity.YardsPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMilePerHour(Vector3 expected)
    {
        Velocity3 quantity = new(expected, UnitOfVelocity.MilePerHour);

        Vector3 actual = quantity.MilesPerHour;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
