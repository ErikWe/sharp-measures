#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Momentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(Momentum3Dataset))]
    public void Impulse(Momentum3 quantity)
    {
        Impulse3 result = quantity.AsImpulse;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }
}
