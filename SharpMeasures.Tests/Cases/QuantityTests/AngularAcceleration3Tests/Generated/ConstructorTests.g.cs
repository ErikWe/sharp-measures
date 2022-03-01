#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        AngularAcceleration3 quantity = AngularAcceleration3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngularAccelerationDataset, AngularAccelerationDataset, AngularAccelerationDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(AngularAcceleration x, AngularAcceleration y, AngularAcceleration z)
    {
        AngularAcceleration3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngularAccelerationDataset, AngularAccelerationDataset, AngularAccelerationDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(AngularAcceleration x, AngularAcceleration y, AngularAcceleration z)
    {
        AngularAcceleration3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularAccelerationDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularAcceleration unit)
    {
        AngularAcceleration3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularAcceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularAcceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularAcceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularAccelerationDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularAcceleration unit)
    {
        AngularAcceleration3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularAcceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularAcceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularAcceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngularAccelerationDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfAngularAcceleration unit)
    {
        AngularAcceleration3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.AngularAcceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.AngularAcceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.AngularAcceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularAccelerationDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularAcceleration unit)
    {
        AngularAcceleration3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularAcceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularAcceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularAcceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularAccelerationDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularAcceleration unit)
    {
        AngularAcceleration3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularAcceleration.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularAcceleration.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularAcceleration.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        AngularAcceleration3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        AngularAcceleration3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        AngularAcceleration3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        AngularAcceleration3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        AngularAcceleration3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
