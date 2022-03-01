#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.FrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfFrequencyDataset>))]
    public void InUnit(Scalar expected, UnitOfFrequency unit)
    {
        Frequency quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPerSecond(Scalar expected)
    {
        Frequency quantity = new(expected, UnitOfFrequency.PerSecond);

        Scalar actual = quantity.PerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPerMinute(Scalar expected)
    {
        Frequency quantity = new(expected, UnitOfFrequency.PerMinute);

        Scalar actual = quantity.PerMinute;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPerHour(Scalar expected)
    {
        Frequency quantity = new(expected, UnitOfFrequency.PerHour);

        Scalar actual = quantity.PerHour;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InHertz(Scalar expected)
    {
        Frequency quantity = new(expected, UnitOfFrequency.Hertz);

        Scalar actual = quantity.Hertz;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilohertz(Scalar expected)
    {
        Frequency quantity = new(expected, UnitOfFrequency.Kilohertz);

        Scalar actual = quantity.Kilohertz;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMegahertz(Scalar expected)
    {
        Frequency quantity = new(expected, UnitOfFrequency.Megahertz);

        Scalar actual = quantity.Megahertz;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InGigahertz(Scalar expected)
    {
        Frequency quantity = new(expected, UnitOfFrequency.Gigahertz);

        Scalar actual = quantity.Gigahertz;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InTerahertz(Scalar expected)
    {
        Frequency quantity = new(expected, UnitOfFrequency.Terahertz);

        Scalar actual = quantity.Terahertz;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
