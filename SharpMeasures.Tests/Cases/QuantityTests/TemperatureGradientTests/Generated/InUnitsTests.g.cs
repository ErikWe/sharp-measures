#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureGradientTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTemperatureGradientDataset>))]
    public void InUnit(Scalar expected, UnitOfTemperatureGradient unit)
    {
        TemperatureGradient quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKelvinPerMetre(Scalar expected)
    {
        TemperatureGradient quantity = new(expected, UnitOfTemperatureGradient.KelvinPerMetre);

        Scalar actual = quantity.KelvinPerMetre;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCelsiusPerMetre(Scalar expected)
    {
        TemperatureGradient quantity = new(expected, UnitOfTemperatureGradient.CelsiusPerMetre);

        Scalar actual = quantity.CelsiusPerMetre;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InRankinePerMetre(Scalar expected)
    {
        TemperatureGradient quantity = new(expected, UnitOfTemperatureGradient.RankinePerMetre);

        Scalar actual = quantity.RankinePerMetre;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFahrenheitPerMetre(Scalar expected)
    {
        TemperatureGradient quantity = new(expected, UnitOfTemperatureGradient.FahrenheitPerMetre);

        Scalar actual = quantity.FahrenheitPerMetre;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFahrenheitPerFoot(Scalar expected)
    {
        TemperatureGradient quantity = new(expected, UnitOfTemperatureGradient.FahrenheitPerFoot);

        Scalar actual = quantity.FahrenheitPerFoot;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
