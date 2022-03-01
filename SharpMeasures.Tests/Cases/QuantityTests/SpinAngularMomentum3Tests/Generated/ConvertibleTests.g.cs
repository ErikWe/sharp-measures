#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(SpinAngularMomentum3Dataset))]
    public void AngularMomentum(SpinAngularMomentum3 quantity)
    {
        AngularMomentum3 result = quantity.AsAngularMomentum;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentum3Dataset))]
    public void OrbitalAngularMomentum(SpinAngularMomentum3 quantity)
    {
        OrbitalAngularMomentum3 result = quantity.AsOrbitalAngularMomentum;

        Assert.Equal(quantity.X, result.X, 2);
        Assert.Equal(quantity.Y, result.Y, 2);
        Assert.Equal(quantity.Z, result.Z, 2);
    }
}
