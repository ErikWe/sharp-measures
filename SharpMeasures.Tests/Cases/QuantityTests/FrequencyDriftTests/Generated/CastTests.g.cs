#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.FrequencyDriftTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(FrequencyDriftDataset))]
    public void ToDouble_ShouldMatchMagnitude(FrequencyDrift quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(FrequencyDriftDataset))]
    public void CastToDouble_ShouldMatchMagnitude(FrequencyDrift quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(FrequencyDriftDataset))]
    public void ToScalar_ShouldMatchMagnitude(FrequencyDrift quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(FrequencyDriftDataset))]
    public void CastToScalar_ShouldMatchMagnitude(FrequencyDrift quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        FrequencyDrift result = FrequencyDrift.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        FrequencyDrift result = (FrequencyDrift)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        FrequencyDrift result = FrequencyDrift.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        FrequencyDrift result = (FrequencyDrift)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
