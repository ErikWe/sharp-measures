#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        AngularAcceleration quantity = AngularAcceleration.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneRadianPerSecondSquared_ShouldMatchUnitScale()
    {
        AngularAcceleration quantity = AngularAcceleration.OneRadianPerSecondSquared;

        Assert.Equal(UnitOfAngularAcceleration.RadianPerSecondSquared.AngularAcceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularAccelerationDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfAngularAcceleration unit)
    {
        AngularAcceleration quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.AngularAcceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularAccelerationDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfAngularAcceleration unit)
    {
        AngularAcceleration quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.AngularAcceleration.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        AngularAcceleration quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        AngularAcceleration quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        AngularAcceleration quantity = AngularAcceleration.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        AngularAcceleration quantity = (AngularAcceleration)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        AngularAcceleration quantity = AngularAcceleration.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        AngularAcceleration quantity = (AngularAcceleration)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
