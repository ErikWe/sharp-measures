#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Rotation3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Rotation3 quantity = Rotation3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngleDataset, AngleDataset, AngleDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(Angle x, Angle y, Angle z)
    {
        Rotation3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AngleDataset, AngleDataset, AngleDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(Angle x, Angle y, Angle z)
    {
        Rotation3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngleDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngle unit)
    {
        Rotation3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Angle.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Angle.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Angle.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngleDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngle unit)
    {
        Rotation3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Angle.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Angle.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Angle.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngleDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfAngle unit)
    {
        Rotation3 quantity = new(vector, unit);

        Assert.Equal(vector.MagnitudeX * unit.Angle.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * unit.Angle.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * unit.Angle.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngleDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngle unit)
    {
        Rotation3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.Angle.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Angle.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Angle.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngleDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngle unit)
    {
        Rotation3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.Angle.Magnitude, quantity.MagnitudeX, 2);
        Assert.Equal(y * unit.Angle.Magnitude, quantity.MagnitudeY, 2);
        Assert.Equal(z * unit.Angle.Magnitude, quantity.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Rotation3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        Rotation3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        Rotation3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Rotation3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        Rotation3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
