#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureDifferenceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(TemperatureDifferenceDataset))]
    public void ToDouble_ShouldMatchMagnitude(TemperatureDifference quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(TemperatureDifferenceDataset))]
    public void CastToDouble_ShouldMatchMagnitude(TemperatureDifference quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(TemperatureDifferenceDataset))]
    public void ToScalar_ShouldMatchMagnitude(TemperatureDifference quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(TemperatureDifferenceDataset))]
    public void CastToScalar_ShouldMatchMagnitude(TemperatureDifference quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        TemperatureDifference result = TemperatureDifference.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        TemperatureDifference result = (TemperatureDifference)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        TemperatureDifference result = TemperatureDifference.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        TemperatureDifference result = (TemperatureDifference)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
