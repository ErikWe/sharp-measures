#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpeedSquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void ToDouble_ShouldMatchMagnitude(SpeedSquared quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void CastToDouble_ShouldMatchMagnitude(SpeedSquared quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void ToScalar_ShouldMatchMagnitude(SpeedSquared quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpeedSquaredDataset))]
    public void CastToScalar_ShouldMatchMagnitude(SpeedSquared quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        SpeedSquared result = SpeedSquared.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        SpeedSquared result = (SpeedSquared)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        SpeedSquared result = SpeedSquared.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        SpeedSquared result = (SpeedSquared)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
