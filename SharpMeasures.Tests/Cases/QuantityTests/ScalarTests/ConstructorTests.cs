namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Double;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Scalar scalar = Scalar.Zero;

        Assert.Equal(0, scalar, 2);
    }

    [Fact]
    public void One_MagnitudeShouldBeOne()
    {
        Scalar scalar = Scalar.One;

        Assert.Equal(1, scalar, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqualToDouble(double a)
    {
        Scalar scalar = new(a);

        Assert.Equal(a, scalar, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Scalar scalar = Scalar.FromDouble(a);

        Assert.Equal(a, scalar, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Scalar scalar = (Scalar)a;

        Assert.Equal(a, scalar, 2);
    }
}