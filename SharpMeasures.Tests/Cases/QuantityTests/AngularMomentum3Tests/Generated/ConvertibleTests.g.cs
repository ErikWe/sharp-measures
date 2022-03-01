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

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(AngularMomentum3Dataset))]
    public void SpinAngularMomentum(AngularMomentum3 quantity)
    {
        SpinAngularMomentum3 result = quantity.AsSpinAngularMomentum;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }
}
