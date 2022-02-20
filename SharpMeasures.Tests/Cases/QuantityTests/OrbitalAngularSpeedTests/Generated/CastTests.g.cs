#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularSpeedDataset))]
    public void ToDouble_ShouldMatchMagnitude(OrbitalAngularSpeed quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularSpeedDataset))]
    public void CastToDouble_ShouldMatchMagnitude(OrbitalAngularSpeed quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularSpeedDataset))]
    public void ToScalar_ShouldMatchMagnitude(OrbitalAngularSpeed quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularSpeedDataset))]
    public void CastToScalar_ShouldMatchMagnitude(OrbitalAngularSpeed quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        OrbitalAngularSpeed result = OrbitalAngularSpeed.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        OrbitalAngularSpeed result = (OrbitalAngularSpeed)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        OrbitalAngularSpeed result = OrbitalAngularSpeed.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        OrbitalAngularSpeed result = (OrbitalAngularSpeed)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
