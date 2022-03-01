#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.WeightTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(WeightDataset))]
    public void Force(Weight quantity)
    {
        Force result = quantity.AsForce;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
