#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ForceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(ForceDataset))]
    public void Weight(Force quantity)
    {
        Weight result = quantity.AsWeight;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
