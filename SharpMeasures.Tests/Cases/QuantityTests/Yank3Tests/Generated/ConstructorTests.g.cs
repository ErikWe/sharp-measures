#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Yank3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Yank3 quantity = Yank3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<YankDataset, YankDataset, YankDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Yank x, Yank y, Yank z)
    {
        Yank3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<YankDataset, YankDataset, YankDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Yank x, Yank y, Yank z)
    {
        Yank3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfYankDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfYank unit)
    {
        Yank3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Yank.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Yank.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Yank.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfYankDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfYank unit)
    {
        Yank3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Yank.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Yank.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Yank.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfYankDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfYank unit)
    {
        Yank3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.Yank.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.Yank.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.Yank.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfYankDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfYank unit)
    {
        Yank3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Yank.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Yank.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Yank.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfYankDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfYank unit)
    {
        Yank3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Yank.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Yank.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Yank.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Yank3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Yank3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Yank3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Yank3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Yank3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
