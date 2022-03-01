#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(AngularAcceleration3Dataset))]
    public void OrbitalAngularAcceleration(AngularAcceleration3 quantity)
    {
        OrbitalAngularAcceleration3 result = quantity.AsOrbitalAngularAcceleration;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(AngularAcceleration3Dataset))]
    public void SpinAngularAcceleration(AngularAcceleration3 quantity)
    {
        SpinAngularAcceleration3 result = quantity.AsSpinAngularAcceleration;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }
}
