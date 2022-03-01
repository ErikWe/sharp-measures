#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Force3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Force3 quantity = Force3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ForceDataset, ForceDataset, ForceDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Force x, Force y, Force z)
    {
        Force3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ForceDataset, ForceDataset, ForceDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Force x, Force y, Force z)
    {
        Force3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfForceDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfForce unit)
    {
        Force3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Force.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Force.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Force.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfForceDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfForce unit)
    {
        Force3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Force.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Force.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Force.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfForceDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfForce unit)
    {
        Force3 quantity = new(vector, unit);

        Assert.Equal(vector.X * unit.Force.Magnitude, quantity.X, 2);
        Assert.Equal(vector.Y * unit.Force.Magnitude, quantity.Y, 2);
        Assert.Equal(vector.Z * unit.Force.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfForceDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfForce unit)
    {
        Force3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Force.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Force.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Force.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfForceDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfForce unit)
    {
        Force3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Force.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Force.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Force.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Force3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Force3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Force3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Force3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Force3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
