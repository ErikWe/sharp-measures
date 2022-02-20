#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpatialFrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void ToDouble_ShouldMatchMagnitude(SpatialFrequency quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void CastToDouble_ShouldMatchMagnitude(SpatialFrequency quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void ToScalar_ShouldMatchMagnitude(SpatialFrequency quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpatialFrequencyDataset))]
    public void CastToScalar_ShouldMatchMagnitude(SpatialFrequency quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        SpatialFrequency result = SpatialFrequency.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        SpatialFrequency result = (SpatialFrequency)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        SpatialFrequency result = SpatialFrequency.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        SpatialFrequency result = (SpatialFrequency)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
