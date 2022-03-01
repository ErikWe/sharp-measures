#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(SpinAngularVelocity3Dataset))]
    public void AngularVelocity(SpinAngularVelocity3 quantity)
    {
        AngularVelocity3 result = quantity.AsAngularVelocity;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularVelocity3Dataset))]
    public void OrbitalAngularVelocity(SpinAngularVelocity3 quantity)
    {
        OrbitalAngularVelocity3 result = quantity.AsOrbitalAngularVelocity;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }
}
