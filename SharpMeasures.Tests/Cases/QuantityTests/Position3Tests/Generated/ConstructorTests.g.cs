#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Position3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Position3 quantity = Position3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<LengthDataset, LengthDataset, LengthDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Length x, Length y, Length z)
    {
        Position3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<LengthDataset, LengthDataset, LengthDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Length x, Length y, Length z)
    {
        Position3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfLengthDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfLength unit)
    {
        Position3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Length.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Length.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Length.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfLengthDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfLength unit)
    {
        Position3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Length.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Length.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Length.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfLengthDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfLength unit)
    {
        Position3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.Length.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.Length.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.Length.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfLengthDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfLength unit)
    {
        Position3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Length.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Length.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Length.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfLengthDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfLength unit)
    {
        Position3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Length.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Length.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Length.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Position3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Position3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Position3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Position3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Position3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
