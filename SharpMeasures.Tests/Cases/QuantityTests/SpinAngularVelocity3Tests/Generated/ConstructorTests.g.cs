#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        SpinAngularVelocity3 quantity = SpinAngularVelocity3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularSpeedDataset, SpinAngularSpeedDataset, SpinAngularSpeedDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(SpinAngularSpeed x, SpinAngularSpeed y, SpinAngularSpeed z)
    {
        SpinAngularVelocity3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularSpeedDataset, SpinAngularSpeedDataset, SpinAngularSpeedDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(SpinAngularSpeed x, SpinAngularSpeed y, SpinAngularSpeed z)
    {
        SpinAngularVelocity3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularVelocityDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularVelocity unit)
    {
        SpinAngularVelocity3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularSpeed.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularSpeed.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularSpeed.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularVelocityDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularVelocity unit)
    {
        SpinAngularVelocity3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularSpeed.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularSpeed.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularSpeed.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngularVelocityDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfAngularVelocity unit)
    {
        SpinAngularVelocity3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.AngularSpeed.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.AngularSpeed.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.AngularSpeed.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularVelocityDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularVelocity unit)
    {
        SpinAngularVelocity3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularSpeed.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularSpeed.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularSpeed.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularVelocityDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularVelocity unit)
    {
        SpinAngularVelocity3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularSpeed.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularSpeed.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularSpeed.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        SpinAngularVelocity3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        SpinAngularVelocity3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        SpinAngularVelocity3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        SpinAngularVelocity3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        SpinAngularVelocity3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}