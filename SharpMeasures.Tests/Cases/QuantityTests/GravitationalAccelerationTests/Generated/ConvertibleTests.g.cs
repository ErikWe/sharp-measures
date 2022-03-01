#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(GravitationalAccelerationDataset))]
    public void Acceleration(GravitationalAcceleration quantity)
    {
        Acceleration result = quantity.AsAcceleration;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
