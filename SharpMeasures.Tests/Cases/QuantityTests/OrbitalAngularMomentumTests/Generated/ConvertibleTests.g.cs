#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularMomentumDataset))]
    public void AngularMomentum(OrbitalAngularMomentum quantity)
    {
        AngularMomentum result = quantity.AsAngularMomentum;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularMomentumDataset))]
    public void SpinAngularMomentum(OrbitalAngularMomentum quantity)
    {
        SpinAngularMomentum result = quantity.AsSpinAngularMomentum;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
