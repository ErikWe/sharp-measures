#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        SpecificAngularMomentum3 quantity = SpecificAngularMomentum3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpecificAngularMomentumDataset, SpecificAngularMomentumDataset, SpecificAngularMomentumDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(SpecificAngularMomentum x, SpecificAngularMomentum y, SpecificAngularMomentum z)
    {
        SpecificAngularMomentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpecificAngularMomentumDataset, SpecificAngularMomentumDataset, SpecificAngularMomentumDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(SpecificAngularMomentum x, SpecificAngularMomentum y, SpecificAngularMomentum z)
    {
        SpecificAngularMomentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfSpecificAngularMomentumDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfSpecificAngularMomentum unit)
    {
        SpecificAngularMomentum3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfSpecificAngularMomentumDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfSpecificAngularMomentum unit)
    {
        SpecificAngularMomentum3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfSpecificAngularMomentumDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfSpecificAngularMomentum unit)
    {
        SpecificAngularMomentum3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfSpecificAngularMomentumDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfSpecificAngularMomentum unit)
    {
        SpecificAngularMomentum3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfSpecificAngularMomentumDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfSpecificAngularMomentum unit)
    {
        SpecificAngularMomentum3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.SpecificAngularMomentum.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        SpecificAngularMomentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        SpecificAngularMomentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        SpecificAngularMomentum3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        SpecificAngularMomentum3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        SpecificAngularMomentum3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
