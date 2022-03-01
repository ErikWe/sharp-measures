#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Momentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Momentum3 quantity = Momentum3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<MomentumDataset, MomentumDataset, MomentumDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Momentum x, Momentum y, Momentum z)
    {
        Momentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<MomentumDataset, MomentumDataset, MomentumDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Momentum x, Momentum y, Momentum z)
    {
        Momentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfMomentumDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfMomentum unit)
    {
        Momentum3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Momentum.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Momentum.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Momentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfMomentumDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfMomentum unit)
    {
        Momentum3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Momentum.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Momentum.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Momentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfMomentumDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfMomentum unit)
    {
        Momentum3 quantity = new(vector, unit);

        Assert.Equal(vector.X * unit.Momentum.Magnitude, quantity.X, 2);
        Assert.Equal(vector.Y * unit.Momentum.Magnitude, quantity.Y, 2);
        Assert.Equal(vector.Z * unit.Momentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfMomentumDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfMomentum unit)
    {
        Momentum3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Momentum.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Momentum.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Momentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfMomentumDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfMomentum unit)
    {
        Momentum3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Momentum.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.Momentum.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.Momentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Momentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Momentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Momentum3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Momentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Momentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
