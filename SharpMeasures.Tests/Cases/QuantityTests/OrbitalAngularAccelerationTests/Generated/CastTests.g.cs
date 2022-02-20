#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularAccelerationDataset))]
    public void ToDouble_ShouldMatchMagnitude(OrbitalAngularAcceleration quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularAccelerationDataset))]
    public void CastToDouble_ShouldMatchMagnitude(OrbitalAngularAcceleration quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularAccelerationDataset))]
    public void ToScalar_ShouldMatchMagnitude(OrbitalAngularAcceleration quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularAccelerationDataset))]
    public void CastToScalar_ShouldMatchMagnitude(OrbitalAngularAcceleration quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        OrbitalAngularAcceleration result = OrbitalAngularAcceleration.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        OrbitalAngularAcceleration result = (OrbitalAngularAcceleration)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        OrbitalAngularAcceleration result = OrbitalAngularAcceleration.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        OrbitalAngularAcceleration result = (OrbitalAngularAcceleration)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
