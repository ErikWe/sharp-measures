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

    [Fact]
    public void Ones_ComponentsShouldBeOne()
    {
        Unhandled3 quantity = Unhandled3.Ones;

        Utility.AssertExtra.AssertEqualComponents((1, 1, 1), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentsShouldBeEqual(Vector3 components)
    {
        Unhandled3 quantity = new(components);

        Utility.AssertExtra.AssertEqualComponents(components.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset, UnhandledDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Unhandled x, Unhandled y, Unhandled z)
    {
        Unhandled3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset, UnhandledDataset>))]
    public void TupleComponents_ComponentMagnitudesShouldBeEqual(Unhandled x, Unhandled y, Unhandled z)
    {
        Unhandled3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Unhandled3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Unhandled3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Unhandled3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Unhandled3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void FromTuple_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Unhandled3 quantity = Unhandled3.FromValueTuple((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void CastedTuple_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Unhandled3 quantity = (Unhandled3)(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void FromVector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Unhandled3 quantity = Unhandled3.FromVector3(vector);

        Utility.AssertExtra.AssertEqualComponents((IVector3Quantity)vector, quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void CastedVector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Unhandled3 quantity = (Unhandled3)vector;

        Utility.AssertExtra.AssertEqualComponents((IVector3Quantity)vector, quantity);
    }
}