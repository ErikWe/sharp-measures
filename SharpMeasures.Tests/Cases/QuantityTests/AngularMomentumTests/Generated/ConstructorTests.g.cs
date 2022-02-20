#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        AngularMomentum quantity = AngularMomentum.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneKilogramSquareMetrePerSecond_ShouldMatchUnitScale()
    {
        AngularMomentum quantity = AngularMomentum.OneKilogramSquareMetrePerSecond;

        Assert.Equal(UnitOfAngularMomentum.KilogramSquareMetrePerSecond.AngularMomentum.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularMomentumDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfAngularMomentum unit)
    {
        AngularMomentum quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.AngularMomentum.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularMomentumDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfAngularMomentum unit)
    {
        AngularMomentum quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.AngularMomentum.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        AngularMomentum quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        AngularMomentum quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        AngularMomentum quantity = AngularMomentum.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        AngularMomentum quantity = (AngularMomentum)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        AngularMomentum quantity = AngularMomentum.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        AngularMomentum quantity = (AngularMomentum)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
