#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(SpinAngularSpeedDataset))]
    public void AngularSpeed(SpinAngularSpeed quantity)
    {
        AngularSpeed result = quantity.AsAngularSpeed;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularSpeedDataset))]
    public void OrbitalAngularSpeed(SpinAngularSpeed quantity)
    {
        OrbitalAngularSpeed result = quantity.AsOrbitalAngularSpeed;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
