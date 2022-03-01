#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(AngularVelocity3Dataset))]
    public void OrbitalAngularVelocity(AngularVelocity3 quantity)
    {
        OrbitalAngularVelocity3 result = quantity.AsOrbitalAngularVelocity;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(AngularVelocity3Dataset))]
    public void SpinAngularVelocity(AngularVelocity3 quantity)
    {
        SpinAngularVelocity3 result = quantity.AsSpinAngularVelocity;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }
}
