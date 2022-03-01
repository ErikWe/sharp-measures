#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularVelocity3Dataset))]
    public void AngularVelocity(OrbitalAngularVelocity3 quantity)
    {
        AngularVelocity3 result = quantity.AsAngularVelocity;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularVelocity3Dataset))]
    public void SpinAngularVelocity(OrbitalAngularVelocity3 quantity)
    {
        SpinAngularVelocity3 result = quantity.AsSpinAngularVelocity;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }
}
