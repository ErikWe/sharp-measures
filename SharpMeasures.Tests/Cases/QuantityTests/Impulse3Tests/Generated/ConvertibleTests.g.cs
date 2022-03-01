#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Impulse3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(Impulse3Dataset))]
    public void Momentum(Impulse3 quantity)
    {
        Momentum3 result = quantity.AsMomentum;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }
}
