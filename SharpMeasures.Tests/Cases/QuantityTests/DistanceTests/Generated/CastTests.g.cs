#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.DistanceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void ToDouble_ShouldMatchMagnitude(Distance quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void CastToDouble_ShouldMatchMagnitude(Distance quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void ToScalar_ShouldMatchMagnitude(Distance quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void CastToScalar_ShouldMatchMagnitude(Distance quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        Distance result = Distance.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        Distance result = (Distance)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        Distance result = Distance.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        Distance result = (Distance)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}