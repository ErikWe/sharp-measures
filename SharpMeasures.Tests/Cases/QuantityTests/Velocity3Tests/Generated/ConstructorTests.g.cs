#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Velocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Velocity3 quantity = Velocity3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpeedDataset, SpeedDataset, SpeedDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Speed x, Speed y, Speed z)
    {
        Velocity3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpeedDataset, SpeedDataset, SpeedDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Speed x, Speed y, Speed z)
    {
        Velocity3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfVelocityDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfVelocity unit)
    {
        Velocity3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Speed.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Speed.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Speed.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfVelocityDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfVelocity unit)
    {
        Velocity3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Speed.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Speed.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Speed.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfVelocityDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfVelocity unit)
    {
        Velocity3 quantity = new(vector, unit);

        Assert.Equal(vector.X * unit.Speed.Magnitude, quantity.X, 2);
        Assert.Equal(vector.Y * unit.Speed.Magnitude, quantity.Y, 2);
        Assert.Equal(vector.Z * unit.Speed.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfVelocityDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfVelocity unit)
    {
        Velocity3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Speed.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Speed.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Speed.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfVelocityDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfVelocity unit)
    {
        Velocity3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Speed.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Speed.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Speed.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Velocity3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Velocity3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Velocity3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Velocity3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Velocity3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
