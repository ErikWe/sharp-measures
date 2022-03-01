#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        AngularMomentum3 quantity = AngularMomentum3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngularMomentumDataset, AngularMomentumDataset, AngularMomentumDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(AngularMomentum x, AngularMomentum y, AngularMomentum z)
    {
        AngularMomentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngularMomentumDataset, AngularMomentumDataset, AngularMomentumDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(AngularMomentum x, AngularMomentum y, AngularMomentum z)
    {
        AngularMomentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularMomentumDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularMomentum unit)
    {
        AngularMomentum3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularMomentum.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.AngularMomentum.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.AngularMomentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularMomentumDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularMomentum unit)
    {
        AngularMomentum3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularMomentum.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.AngularMomentum.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.AngularMomentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngularMomentumDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfAngularMomentum unit)
    {
        AngularMomentum3 quantity = new(vector, unit);

        Assert.Equal(vector.X * unit.AngularMomentum.Magnitude, quantity.X, 2);
        Assert.Equal(vector.Y * unit.AngularMomentum.Magnitude, quantity.Y, 2);
        Assert.Equal(vector.Z * unit.AngularMomentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularMomentumDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularMomentum unit)
    {
        AngularMomentum3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularMomentum.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.AngularMomentum.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.AngularMomentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularMomentumDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularMomentum unit)
    {
        AngularMomentum3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularMomentum.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.AngularMomentum.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.AngularMomentum.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        AngularMomentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        AngularMomentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        AngularMomentum3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        AngularMomentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        AngularMomentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
