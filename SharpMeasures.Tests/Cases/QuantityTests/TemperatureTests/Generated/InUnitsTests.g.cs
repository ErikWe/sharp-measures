#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTemperatureDataset>))]
    public void InUnit(Scalar expected, UnitOfTemperature unit)
    {
        Temperature quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKelvin(Scalar expected)
    {
        Temperature quantity = new(expected, UnitOfTemperature.Kelvin);

        Scalar actual = quantity.Kelvin;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCelsius(Scalar expected)
    {
        Temperature quantity = new(expected, UnitOfTemperature.Celsius);

        Scalar actual = quantity.Celsius;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InRankine(Scalar expected)
    {
        Temperature quantity = new(expected, UnitOfTemperature.Rankine);

        Scalar actual = quantity.Rankine;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFahrenheit(Scalar expected)
    {
        Temperature quantity = new(expected, UnitOfTemperature.Fahrenheit);

        Scalar actual = quantity.Fahrenheit;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
