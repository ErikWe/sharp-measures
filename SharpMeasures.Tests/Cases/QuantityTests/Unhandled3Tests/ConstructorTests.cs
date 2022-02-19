namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Unhandled3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Unhandled3 quantity = Unhandled3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentsShouldBeEqual(Vector3 components)
    {
        Unhandled3 quantity = new(components);

        Assert.Equal(components.X, quantity.X);
        Assert.Equal(components.Y, quantity.Y);
        Assert.Equal(components.Z, quantity.Z);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset, UnhandledDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Unhandled x, Unhandled y, Unhandled z)
    {
        Unhandled3 quantity = new(x, y, z);

        Assert.Equal(x.Magnitude, quantity.X);
        Assert.Equal(y.Magnitude, quantity.Y);
        Assert.Equal(z.Magnitude, quantity.Z);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset, UnhandledDataset>))]
    public void TupleComponents_ComponentMagnitudesShouldBeEqual(Unhandled x, Unhandled y, Unhandled z)
    {
        Unhandled3 quantity = new((x, y, z));

        Assert.Equal(x.Magnitude, quantity.X);
        Assert.Equal(y.Magnitude, quantity.Y);
        Assert.Equal(z.Magnitude, quantity.Z);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Unhandled3 quantity = new(x, y, z);

        Assert.Equal(x, quantity.X);
        Assert.Equal(y, quantity.Y);
        Assert.Equal(z, quantity.Z);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Unhandled3 quantity = new((x, y, z));

        Assert.Equal(x, quantity.X);
        Assert.Equal(y, quantity.Y);
        Assert.Equal(z, quantity.Z);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Unhandled3 quantity = new(x, y, z);

        Assert.Equal(x, quantity.X);
        Assert.Equal(y, quantity.Y);
        Assert.Equal(z, quantity.Z);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Unhandled3 quantity = new((x, y, z));

        Assert.Equal(x, quantity.X);
        Assert.Equal(y, quantity.Y);
        Assert.Equal(z, quantity.Z);
    }
}