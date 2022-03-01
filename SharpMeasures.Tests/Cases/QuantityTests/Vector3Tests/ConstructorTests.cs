namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Vector3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Vector3 vector = Vector3.Zero;

        Assert.Equal(0, vector.Magnitude().Magnitude);
    }

    [Fact]
    public void Ones_ComponentsShouldBeOne()
    {
        Vector3 vector = Vector3.Ones;

        Utility.AssertExtra.AssertEqualComponents((1, 1, 1), vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Vector3 vector = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleComponents_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Vector3 vector = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Vector3 vector = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Vector3 vector = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), vector);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void FromTuple_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Vector3 quantity = Vector3.FromValueTuple((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void CastedTuple_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Vector3 quantity = (Vector3)(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}