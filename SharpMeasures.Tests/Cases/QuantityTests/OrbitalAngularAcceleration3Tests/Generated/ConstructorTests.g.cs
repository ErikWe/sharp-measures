#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        OrbitalAngularAcceleration3 quantity = OrbitalAngularAcceleration3.Zero;

        Assert.Equal(0, quantity.Magnitude().Magnitude);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularAccelerationDataset, OrbitalAngularAccelerationDataset, OrbitalAngularAccelerationDataset>))]
    public void TupleComponents_ComponentsShouldBeEqual(OrbitalAngularAcceleration x, OrbitalAngularAcceleration y, OrbitalAngularAcceleration z)
    {
        OrbitalAngularAcceleration3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularAccelerationDataset, OrbitalAngularAccelerationDataset, OrbitalAngularAccelerationDataset>))]
    public void Components_ComponentMagnitudesShouldBeEqual(OrbitalAngularAcceleration x, OrbitalAngularAcceleration y, OrbitalAngularAcceleration z)
    {
        OrbitalAngularAcceleration3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x.Magnitude, y.Magnitude, z.Magnitude), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularAccelerationDataset>))]
    public void TupleScalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularAcceleration unit)
    {
        OrbitalAngularAcceleration3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularAcceleration.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.AngularAcceleration.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.AngularAcceleration.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset, UnitOfAngularAccelerationDataset>))]
    public void Scalars_Unit_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z, UnitOfAngularAcceleration unit)
    {
        OrbitalAngularAcceleration3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularAcceleration.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.AngularAcceleration.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.AngularAcceleration.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngularAccelerationDataset>))]
    public void Vector3_Unit_ComponentMagnitudesShouldBeEqual(Vector3 vector, UnitOfAngularAcceleration unit)
    {
        OrbitalAngularAcceleration3 quantity = new(vector, unit);

        Assert.Equal(vector.X * unit.AngularAcceleration.Magnitude, quantity.X, 2);
        Assert.Equal(vector.Y * unit.AngularAcceleration.Magnitude, quantity.Y, 2);
        Assert.Equal(vector.Z * unit.AngularAcceleration.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularAccelerationDataset>))]
    public void TupleDoubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularAcceleration unit)
    {
        OrbitalAngularAcceleration3 quantity = new((x, y, z), unit);

        Assert.Equal(x * unit.AngularAcceleration.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.AngularAcceleration.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.AngularAcceleration.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset, UnitOfAngularAccelerationDataset>))]
    public void Doubles_Unit_ComponentMagnitudesShouldBeEqual(double x, double y, double z, UnitOfAngularAcceleration unit)
    {
        OrbitalAngularAcceleration3 quantity = new(x, y, z, unit);

        Assert.Equal(x * unit.AngularAcceleration.Magnitude, quantity.X, 2);
        Assert.Equal(y * unit.AngularAcceleration.Magnitude, quantity.Y, 2);
        Assert.Equal(z * unit.AngularAcceleration.Magnitude, quantity.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void TupleScalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        OrbitalAngularAcceleration3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void Scalars_ComponentMagnitudesShouldBeEqual(Scalar x, Scalar y, Scalar z)
    {
        OrbitalAngularAcceleration3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Vector3_ComponentMagnitudesShouldBeEqual(Vector3 vector)
    {
        OrbitalAngularAcceleration3 quantity = new(vector);

        Utility.AssertExtra.AssertEqualComponents(vector.ToValueTuple(), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void TupleDoubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        OrbitalAngularAcceleration3 quantity = new((x, y, z));

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void Doubles_ComponentMagnitudesShouldBeEqual(double x, double y, double z)
    {
        OrbitalAngularAcceleration3 quantity = new(x, y, z);

        Utility.AssertExtra.AssertEqualComponents((x, y, z), quantity);
    }
}
