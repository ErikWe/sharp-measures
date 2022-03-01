#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void OrbitalAngularAcceleration(AngularAcceleration quantity)
    {
        OrbitalAngularAcceleration result = quantity.AsOrbitalAngularAcceleration;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void SpinAngularAcceleration(AngularAcceleration quantity)
    {
        SpinAngularAcceleration result = quantity.AsSpinAngularAcceleration;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
