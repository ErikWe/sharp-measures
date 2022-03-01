#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.LengthTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(LengthDataset))]
    public void Distance(Length quantity)
    {
        Distance result = quantity.AsDistance;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
