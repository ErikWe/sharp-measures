namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Unhandled quantity = Unhandled.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void One_MagnitudeShouldBeOne()
    {
        Unhandled quantity = Unhandled.One;

        Assert.Equal(1, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar scalar)
    {
        Unhandled quantity = new(scalar);

        Assert.Equal(scalar, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double a)
    {
        Unhandled quantity = new(a);

        Assert.Equal(a, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Unhandled quantity = Unhandled.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Unhandled quantity = (Unhandled)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Unhandled quantity = Unhandled.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Unhandled quantity = (Unhandled)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}