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

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }
}
