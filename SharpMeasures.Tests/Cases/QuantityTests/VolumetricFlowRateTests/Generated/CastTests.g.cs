#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.VolumetricFlowRateTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(VolumetricFlowRateDataset))]
    public void ToDouble_ShouldMatchMagnitude(VolumetricFlowRate quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(VolumetricFlowRateDataset))]
    public void CastToDouble_ShouldMatchMagnitude(VolumetricFlowRate quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(VolumetricFlowRateDataset))]
    public void ToScalar_ShouldMatchMagnitude(VolumetricFlowRate quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(VolumetricFlowRateDataset))]
    public void CastToScalar_ShouldMatchMagnitude(VolumetricFlowRate quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        VolumetricFlowRate result = VolumetricFlowRate.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        VolumetricFlowRate result = (VolumetricFlowRate)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        VolumetricFlowRate result = VolumetricFlowRate.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        VolumetricFlowRate result = (VolumetricFlowRate)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
