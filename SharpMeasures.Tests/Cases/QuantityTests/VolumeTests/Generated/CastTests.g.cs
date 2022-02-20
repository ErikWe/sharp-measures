#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.VolumeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(VolumeDataset))]
    public void ToDouble_ShouldMatchMagnitude(Volume quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(VolumeDataset))]
    public void CastToDouble_ShouldMatchMagnitude(Volume quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(VolumeDataset))]
    public void ToScalar_ShouldMatchMagnitude(Volume quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(VolumeDataset))]
    public void CastToScalar_ShouldMatchMagnitude(Volume quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        Volume result = Volume.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        Volume result = (Volume)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        Volume result = Volume.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        Volume result = (Volume)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
