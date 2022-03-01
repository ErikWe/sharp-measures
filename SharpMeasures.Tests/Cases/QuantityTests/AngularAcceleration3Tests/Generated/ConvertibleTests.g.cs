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

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(AngularAcceleration3Dataset))]
    public void SpinAngularAcceleration(AngularAcceleration3 quantity)
    {
        SpinAngularAcceleration3 result = quantity.AsSpinAngularAcceleration;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }
}
