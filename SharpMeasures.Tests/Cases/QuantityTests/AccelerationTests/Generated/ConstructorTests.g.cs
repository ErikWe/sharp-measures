#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Acceleration quantity = Acceleration.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void StandardGravity_ShouldMatchUnitScale()
    {
        Acceleration quantity = Acceleration.StandardGravity;

        Assert.Equal(UnitOfAcceleration.StandardGravity.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMetrePerSecondSquared_ShouldMatchUnitScale()
    {
        Acceleration quantity = Acceleration.OneMetrePerSecondSquared;

        Assert.Equal(UnitOfAcceleration.MetrePerSecondSquared.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFootPerSecondSquared_ShouldMatchUnitScale()
    {
        Acceleration quantity = Acceleration.OneFootPerSecondSquared;

        Assert.Equal(UnitOfAcceleration.FootPerSecondSquared.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAccelerationDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfAcceleration unit)
    {
        Acceleration quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAccelerationDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfAcceleration unit)
    {
        Acceleration quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Acceleration quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Acceleration quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Acceleration quantity = Acceleration.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Acceleration quantity = (Acceleration)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Acceleration quantity = Acceleration.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Acceleration quantity = (Acceleration)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
