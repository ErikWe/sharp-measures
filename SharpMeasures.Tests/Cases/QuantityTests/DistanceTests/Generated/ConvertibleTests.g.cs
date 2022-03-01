#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.DistanceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void Length(Distance quantity)
    {
        Length result = quantity.AsLength;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
