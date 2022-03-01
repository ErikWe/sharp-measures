#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void AngularMomentum(SpinAngularMomentum quantity)
    {
        AngularMomentum result = quantity.AsAngularMomentum;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void OrbitalAngularMomentum(SpinAngularMomentum quantity)
    {
        OrbitalAngularMomentum result = quantity.AsOrbitalAngularMomentum;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
