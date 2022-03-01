#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        GravitationalAcceleration3 quantity = GravitationalAcceleration3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<GravitationalAccelerationDataset, GravitationalAccelerationDataset, GravitationalAccelerationDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(GravitationalAcceleration x, GravitationalAcceleration y, GravitationalAcceleration z)
    {
        GravitationalAcceleration3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<GravitationalAccelerationDataset, GravitationalAccelerationDataset, GravitationalAccelerationDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(GravitationalAcceleration x, GravitationalAcceleration y, GravitationalAcceleration z)
    {
        GravitationalAcceleration3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAccelerationDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAcceleration unit)
    {
        GravitationalAcceleration3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Acceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Acceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Acceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAccelerationDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAcceleration unit)
    {
        GravitationalAcceleration3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Acceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Acceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Acceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAccelerationDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfAcceleration unit)
    {
        GravitationalAcceleration3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.Acceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.Acceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.Acceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAccelerationDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAcceleration unit)
    {
        GravitationalAcceleration3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Acceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Acceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Acceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAccelerationDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAcceleration unit)
    {
        GravitationalAcceleration3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Acceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Acceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Acceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        GravitationalAcceleration3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        GravitationalAcceleration3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        GravitationalAcceleration3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        GravitationalAcceleration3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        GravitationalAcceleration3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
