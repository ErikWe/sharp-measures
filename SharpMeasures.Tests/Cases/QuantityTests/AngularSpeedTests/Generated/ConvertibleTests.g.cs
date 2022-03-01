#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(AngularSpeedDataset))]
    public void OrbitalAngularSpeed(AngularSpeed quantity)
    {
        OrbitalAngularSpeed result = quantity.AsOrbitalAngularSpeed;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(AngularSpeedDataset))]
    public void SpinAngularSpeed(AngularSpeed quantity)
    {
        SpinAngularSpeed result = quantity.AsSpinAngularSpeed;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
