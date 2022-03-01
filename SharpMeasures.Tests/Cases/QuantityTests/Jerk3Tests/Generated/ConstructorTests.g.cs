#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Jerk3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Jerk3 quantity = Jerk3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<JerkDataset, JerkDataset, JerkDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Jerk x, Jerk y, Jerk z)
    {
        Jerk3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<JerkDataset, JerkDataset, JerkDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Jerk x, Jerk y, Jerk z)
    {
        Jerk3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfJerkDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfJerk unit)
    {
        Jerk3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Jerk.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Jerk.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Jerk.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfJerkDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfJerk unit)
    {
        Jerk3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Jerk.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Jerk.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Jerk.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfJerkDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfJerk unit)
    {
        Jerk3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.Jerk.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.Jerk.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.Jerk.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfJerkDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfJerk unit)
    {
        Jerk3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Jerk.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Jerk.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Jerk.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfJerkDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfJerk unit)
    {
        Jerk3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Jerk.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Jerk.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Jerk.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Jerk3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Jerk3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Jerk3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Jerk3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Jerk3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
