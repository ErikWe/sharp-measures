#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureGradientTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(TemperatureGradientDataset))]
    public void ToDouble_ShouldMatchMagnitude(TemperatureGradient quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(TemperatureGradientDataset))]
    public void CastToDouble_ShouldMatchMagnitude(TemperatureGradient quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(TemperatureGradientDataset))]
    public void ToScalar_ShouldMatchMagnitude(TemperatureGradient quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(TemperatureGradientDataset))]
    public void CastToScalar_ShouldMatchMagnitude(TemperatureGradient quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        TemperatureGradient result = TemperatureGradient.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        TemperatureGradient result = (TemperatureGradient)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        TemperatureGradient result = TemperatureGradient.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        TemperatureGradient result = (TemperatureGradient)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
