#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularMomentum3Dataset))]
    public void AngularMomentum(OrbitalAngularMomentum3 quantity)
    {
        AngularMomentum3 result = quantity.AsAngularMomentum;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularMomentum3Dataset))]
    public void SpinAngularMomentum(OrbitalAngularMomentum3 quantity)
    {
        SpinAngularMomentum3 result = quantity.AsSpinAngularMomentum;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }
}
