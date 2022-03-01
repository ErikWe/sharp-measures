#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularAccelerationDataset))]
    public void AngularAcceleration(OrbitalAngularAcceleration quantity)
    {
        AngularAcceleration result = quantity.AsAngularAcceleration;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularAccelerationDataset))]
    public void SpinAngularAcceleration(OrbitalAngularAcceleration quantity)
    {
        SpinAngularAcceleration result = quantity.AsSpinAngularAcceleration;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
