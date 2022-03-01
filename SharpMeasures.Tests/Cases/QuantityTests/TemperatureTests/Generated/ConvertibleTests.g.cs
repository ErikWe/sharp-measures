#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(TemperatureDataset))]
    public void TemperatureDifference(Temperature quantity)
    {
        TemperatureDifference result = quantity.AsTemperatureDifference;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
