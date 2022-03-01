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

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentum3Dataset))]
    public void OrbitalAngularMomentum(SpinAngularMomentum3 quantity)
    {
        OrbitalAngularMomentum3 result = quantity.AsOrbitalAngularMomentum;

        Assert.Equal(quantity.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ, result.MagnitudeZ, 2);
    }
}
