#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularAcceleration3Dataset))]
    public void AngularAcceleration(OrbitalAngularAcceleration3 quantity)
    {
        AngularAcceleration3 result = quantity.AsAngularAcceleration;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularAcceleration3Dataset))]
    public void SpinAngularAcceleration(OrbitalAngularAcceleration3 quantity)
    {
        SpinAngularAcceleration3 result = quantity.AsSpinAngularAcceleration;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }
}
