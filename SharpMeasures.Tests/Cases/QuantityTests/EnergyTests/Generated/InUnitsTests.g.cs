#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.EnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfEnergyDataset>))]
    public void InUnit(Scalar expected, UnitOfEnergy unit)
    {
        Energy quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InJoule(Scalar expected)
    {
        Energy quantity = new(expected, UnitOfEnergy.Joule);

        Scalar actual = quantity.Joules;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilojoule(Scalar expected)
    {
        Energy quantity = new(expected, UnitOfEnergy.Kilojoule);

        Scalar actual = quantity.Kilojoules;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMegajoule(Scalar expected)
    {
        Energy quantity = new(expected, UnitOfEnergy.Megajoule);

        Scalar actual = quantity.Megajoules;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InGigajoule(Scalar expected)
    {
        Energy quantity = new(expected, UnitOfEnergy.Gigajoule);

        Scalar actual = quantity.Gigajoules;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilowattHour(Scalar expected)
    {
        Energy quantity = new(expected, UnitOfEnergy.KilowattHour);

        Scalar actual = quantity.KilowattHours;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCalorie(Scalar expected)
    {
        Energy quantity = new(expected, UnitOfEnergy.Calorie);

        Scalar actual = quantity.Calories;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilocalorie(Scalar expected)
    {
        Energy quantity = new(expected, UnitOfEnergy.Kilocalorie);

        Scalar actual = quantity.Kilocalories;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
