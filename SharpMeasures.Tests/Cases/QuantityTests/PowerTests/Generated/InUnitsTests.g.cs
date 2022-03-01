#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PowerTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfPowerDataset>))]
    public void InUnit(Scalar expected, UnitOfPower unit)
    {
        Power quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InWatt(Scalar expected)
    {
        Power quantity = new(expected, UnitOfPower.Watt);

        Scalar actual = quantity.Watts;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilowatt(Scalar expected)
    {
        Power quantity = new(expected, UnitOfPower.Kilowatt);

        Scalar actual = quantity.Kilowatts;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMegawatt(Scalar expected)
    {
        Power quantity = new(expected, UnitOfPower.Megawatt);

        Scalar actual = quantity.Megawatts;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InGigawatt(Scalar expected)
    {
        Power quantity = new(expected, UnitOfPower.Gigawatt);

        Scalar actual = quantity.Gigawatts;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InTerawatt(Scalar expected)
    {
        Power quantity = new(expected, UnitOfPower.Terawatt);

        Scalar actual = quantity.Terawatts;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
