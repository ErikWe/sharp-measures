#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MassFlowRateTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(MassFlowRateDataset))]
    public void ToDouble_ShouldMatchMagnitude(MassFlowRate quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(MassFlowRateDataset))]
    public void CastToDouble_ShouldMatchMagnitude(MassFlowRate quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(MassFlowRateDataset))]
    public void ToScalar_ShouldMatchMagnitude(MassFlowRate quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(MassFlowRateDataset))]
    public void CastToScalar_ShouldMatchMagnitude(MassFlowRate quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        MassFlowRate result = MassFlowRate.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        MassFlowRate result = (MassFlowRate)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        MassFlowRate result = MassFlowRate.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        MassFlowRate result = (MassFlowRate)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
