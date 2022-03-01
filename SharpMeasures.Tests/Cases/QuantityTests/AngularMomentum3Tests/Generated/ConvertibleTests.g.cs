#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(AngularMomentum3Dataset))]
    public void OrbitalAngularMomentum(AngularMomentum3 quantity)
    {
        OrbitalAngularMomentum3 result = quantity.AsOrbitalAngularMomentum;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(AngularMomentum3Dataset))]
    public void SpinAngularMomentum(AngularMomentum3 quantity)
    {
        SpinAngularMomentum3 result = quantity.AsSpinAngularMomentum;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }
}
