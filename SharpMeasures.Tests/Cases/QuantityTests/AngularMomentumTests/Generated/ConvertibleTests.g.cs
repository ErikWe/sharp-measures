#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(AngularMomentumDataset))]
    public void OrbitalAngularMomentum(AngularMomentum quantity)
    {
        OrbitalAngularMomentum result = quantity.AsOrbitalAngularMomentum;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(AngularMomentumDataset))]
    public void SpinAngularMomentum(AngularMomentum quantity)
    {
        SpinAngularMomentum result = quantity.AsSpinAngularMomentum;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
