#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(MomentumDataset))]
    public void Impulse(Momentum quantity)
    {
        Impulse result = quantity.AsImpulse;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
