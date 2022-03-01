#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConvertibleTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularSpeedDataset))]
    public void AngularSpeed(OrbitalAngularSpeed quantity)
    {
        AngularSpeed result = quantity.AsAngularSpeed;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularSpeedDataset))]
    public void SpinAngularSpeed(OrbitalAngularSpeed quantity)
    {
        SpinAngularSpeed result = quantity.AsSpinAngularSpeed;

        Assert.Equal(quantity.Magnitude, result.Magnitude, 2);
    }
}
