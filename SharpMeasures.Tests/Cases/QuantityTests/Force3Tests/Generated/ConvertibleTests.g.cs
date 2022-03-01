#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Force3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(Force3Dataset))]
    public void Weight(Force3 quantity)
    {
        Weight3 result = quantity.AsWeight;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }
}
