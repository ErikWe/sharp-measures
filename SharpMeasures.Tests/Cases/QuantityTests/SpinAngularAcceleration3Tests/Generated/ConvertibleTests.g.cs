#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(SpinAngularAcceleration3Dataset))]
    public void AngularAcceleration(SpinAngularAcceleration3 quantity)
    {
        AngularAcceleration3 result = quantity.AsAngularAcceleration;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularAcceleration3Dataset))]
    public void OrbitalAngularAcceleration(SpinAngularAcceleration3 quantity)
    {
        OrbitalAngularAcceleration3 result = quantity.AsOrbitalAngularAcceleration;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }
}
