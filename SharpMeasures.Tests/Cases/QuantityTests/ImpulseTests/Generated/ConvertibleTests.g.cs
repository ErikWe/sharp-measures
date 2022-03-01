#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ImpulseTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(ImpulseDataset))]
    public void Momentum(Impulse quantity)
    {
        Momentum result = quantity.AsMomentum;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
