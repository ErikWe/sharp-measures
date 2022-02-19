namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Double;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;
using ErikWe.SharpMeasures.Tests.Datasets.Unhandled;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void ToDouble_ShouldMatchMagnitude(Unhandled quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void CastToDouble_ShouldMatchMagnitude(Unhandled quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void ToScalar_ShouldMatchMagnitude(Unhandled quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void CastToScalar_ShouldMatchMagnitude(Unhandled quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        Unhandled result = Unhandled.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        Unhandled result = (Unhandled)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        Unhandled result = Unhandled.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        Unhandled result = (Unhandled)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
