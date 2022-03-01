#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureDifferenceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(TemperatureDifferenceDataset))]
    public void Temperature(TemperatureDifference quantity)
    {
        Temperature result = quantity.AsTemperature;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
