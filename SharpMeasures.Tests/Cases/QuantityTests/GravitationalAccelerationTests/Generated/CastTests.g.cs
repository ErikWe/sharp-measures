#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(GravitationalAccelerationDataset))]
    public void ToDouble_ShouldMatchMagnitude(GravitationalAcceleration quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(GravitationalAccelerationDataset))]
    public void CastToDouble_ShouldMatchMagnitude(GravitationalAcceleration quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(GravitationalAccelerationDataset))]
    public void ToScalar_ShouldMatchMagnitude(GravitationalAcceleration quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(GravitationalAccelerationDataset))]
    public void CastToScalar_ShouldMatchMagnitude(GravitationalAcceleration quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        GravitationalAcceleration result = GravitationalAcceleration.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        GravitationalAcceleration result = (GravitationalAcceleration)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        GravitationalAcceleration result = GravitationalAcceleration.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        GravitationalAcceleration result = (GravitationalAcceleration)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
