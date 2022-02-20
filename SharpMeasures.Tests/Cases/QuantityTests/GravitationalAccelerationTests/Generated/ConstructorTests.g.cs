#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        GravitationalAcceleration quantity = GravitationalAcceleration.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void StandardGravity_ShouldMatchUnitScale()
    {
        GravitationalAcceleration quantity = GravitationalAcceleration.StandardGravity;

        Assert.Equal(UnitOfAcceleration.StandardGravity.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMetrePerSecondSquared_ShouldMatchUnitScale()
    {
        GravitationalAcceleration quantity = GravitationalAcceleration.OneMetrePerSecondSquared;

        Assert.Equal(UnitOfAcceleration.MetrePerSecondSquared.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFootPerSecondSquared_ShouldMatchUnitScale()
    {
        GravitationalAcceleration quantity = GravitationalAcceleration.OneFootPerSecondSquared;

        Assert.Equal(UnitOfAcceleration.FootPerSecondSquared.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAccelerationDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfAcceleration unit)
    {
        GravitationalAcceleration quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAccelerationDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfAcceleration unit)
    {
        GravitationalAcceleration quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Acceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        GravitationalAcceleration quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        GravitationalAcceleration quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        GravitationalAcceleration quantity = GravitationalAcceleration.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        GravitationalAcceleration quantity = (GravitationalAcceleration)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        GravitationalAcceleration quantity = GravitationalAcceleration.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        GravitationalAcceleration quantity = (GravitationalAcceleration)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
