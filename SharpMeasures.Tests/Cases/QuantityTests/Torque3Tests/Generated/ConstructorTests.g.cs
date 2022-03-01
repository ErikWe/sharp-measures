#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Torque3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Torque3 quantity = Torque3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TorqueDataset, TorqueDataset, TorqueDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Torque x, Torque y, Torque z)
    {
        Torque3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TorqueDataset, TorqueDataset, TorqueDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Torque x, Torque y, Torque z)
    {
        Torque3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfTorqueDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfTorque unit)
    {
        Torque3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Torque.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Torque.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Torque.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfTorqueDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfTorque unit)
    {
        Torque3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Torque.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Torque.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Torque.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfTorqueDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfTorque unit)
    {
        Torque3 quantity = new(vector, unit);

        Assert.Equal(vector.X * unit.Torque.Magnitude, quantity.X, 2);
        Assert.Equal(vector.Y * unit.Torque.Magnitude, quantity.Y, 2);
        Assert.Equal(vector.Z * unit.Torque.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfTorqueDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfTorque unit)
    {
        Torque3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Torque.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Torque.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Torque.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfTorqueDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfTorque unit)
    {
        Torque3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Torque.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Torque.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Torque.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Torque3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Torque3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Torque3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Torque3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Torque3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
