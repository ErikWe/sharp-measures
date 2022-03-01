#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Impulse3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Impulse3 quantity = Impulse3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ImpulseDataset, ImpulseDataset, ImpulseDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Impulse x, Impulse y, Impulse z)
    {
        Impulse3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ImpulseDataset, ImpulseDataset, ImpulseDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Impulse x, Impulse y, Impulse z)
    {
        Impulse3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfImpulseDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfImpulse unit)
    {
        Impulse3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Impulse.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Impulse.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Impulse.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfImpulseDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfImpulse unit)
    {
        Impulse3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Impulse.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Impulse.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Impulse.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfImpulseDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfImpulse unit)
    {
        Impulse3 quantity = new(vector, unit);

        Assert.Equal(vector.X * unit.Impulse.Magnitude, quantity.X, 2);
        Assert.Equal(vector.Y * unit.Impulse.Magnitude, quantity.Y, 2);
        Assert.Equal(vector.Z * unit.Impulse.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfImpulseDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfImpulse unit)
    {
        Impulse3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Impulse.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Impulse.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Impulse.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfImpulseDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfImpulse unit)
    {
        Impulse3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Impulse.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Impulse.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Impulse.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Impulse3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Impulse3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Impulse3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Impulse3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Impulse3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
