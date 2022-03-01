#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(SpinAngularAccelerationDataset))]
    public void AngularAcceleration(SpinAngularAcceleration quantity)
    {
        AngularAcceleration result = quantity.AsAngularAcceleration;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularAccelerationDataset))]
    public void OrbitalAngularAcceleration(SpinAngularAcceleration quantity)
    {
        OrbitalAngularAcceleration result = quantity.AsOrbitalAngularAcceleration;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
