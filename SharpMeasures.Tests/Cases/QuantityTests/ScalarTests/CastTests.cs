namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Double;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void ToDouble_ShouldMatchMagnitude(Scalar scalar)
    {
        double result = scalar.ToDouble();

        Assert.Equal(scalar.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastToDouble_ShouldMatchMagnitude(Scalar scalar)
    {
        double result = scalar;

        Assert.Equal(scalar.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        Scalar result = Scalar.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        Scalar result = (Scalar)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
