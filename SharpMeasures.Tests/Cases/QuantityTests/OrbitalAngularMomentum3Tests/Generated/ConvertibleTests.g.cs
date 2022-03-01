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

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularMomentum3Dataset))]
    public void SpinAngularMomentum(OrbitalAngularMomentum3 quantity)
    {
        SpinAngularMomentum3 result = quantity.AsSpinAngularMomentum;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }
}
