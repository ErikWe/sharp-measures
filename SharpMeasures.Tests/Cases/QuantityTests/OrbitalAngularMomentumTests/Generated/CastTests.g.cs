#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(OrbitalAngularMomentumDataset))]
    public void ToDouble_ShouldMatchMagnitude(OrbitalAngularMomentum quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularMomentumDataset))]
    public void CastToDouble_ShouldMatchMagnitude(OrbitalAngularMomentum quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularMomentumDataset))]
    public void ToScalar_ShouldMatchMagnitude(OrbitalAngularMomentum quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(OrbitalAngularMomentumDataset))]
    public void CastToScalar_ShouldMatchMagnitude(OrbitalAngularMomentum quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        OrbitalAngularMomentum result = OrbitalAngularMomentum.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        OrbitalAngularMomentum result = (OrbitalAngularMomentum)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        OrbitalAngularMomentum result = OrbitalAngularMomentum.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        OrbitalAngularMomentum result = (OrbitalAngularMomentum)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
