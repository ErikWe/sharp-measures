#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Absement3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Absement3 quantity = Absement3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AbsementDataset, AbsementDataset, AbsementDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Absement x, Absement y, Absement z)
    {
        Absement3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AbsementDataset, AbsementDataset, AbsementDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Absement x, Absement y, Absement z)
    {
        Absement3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAbsementDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAbsement unit)
    {
        Absement3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Absement.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Absement.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Absement.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAbsementDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAbsement unit)
    {
        Absement3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Absement.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Absement.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Absement.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAbsementDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfAbsement unit)
    {
        Absement3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.Absement.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.Absement.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.Absement.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAbsementDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAbsement unit)
    {
        Absement3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Absement.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Absement.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Absement.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAbsementDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAbsement unit)
    {
        Absement3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Absement.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Absement.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Absement.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Absement3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Absement3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Absement3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Absement3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Absement3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
