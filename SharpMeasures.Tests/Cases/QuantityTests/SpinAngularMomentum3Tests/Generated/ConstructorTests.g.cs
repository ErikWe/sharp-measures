#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        SpinAngularMomentum3 quantity = SpinAngularMomentum3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularMomentumDataset, SpinAngularMomentumDataset, SpinAngularMomentumDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(SpinAngularMomentum x, SpinAngularMomentum y, SpinAngularMomentum z)
    {
        SpinAngularMomentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularMomentumDataset, SpinAngularMomentumDataset, SpinAngularMomentumDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(SpinAngularMomentum x, SpinAngularMomentum y, SpinAngularMomentum z)
    {
        SpinAngularMomentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularMomentumDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularMomentum unit)
    {
        SpinAngularMomentum3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularMomentumDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularMomentum unit)
    {
        SpinAngularMomentum3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngularMomentumDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfAngularMomentum unit)
    {
        SpinAngularMomentum3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.AngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.AngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.AngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularMomentumDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularMomentum unit)
    {
        SpinAngularMomentum3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularMomentumDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularMomentum unit)
    {
        SpinAngularMomentum3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.AngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.AngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        SpinAngularMomentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        SpinAngularMomentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        SpinAngularMomentum3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        SpinAngularMomentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        SpinAngularMomentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
