#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        SpecificAngularMomentum quantity = SpecificAngularMomentum.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneSquareMetrePerSecond_ShouldMatchUnitScale()
    {
        SpecificAngularMomentum quantity = SpecificAngularMomentum.OneSquareMetrePerSecond;

        Assert.Equal(UnitOfSpecificAngularMomentum.SquareMetrePerSecond.SpecificAngularMomentum.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSpecificAngularMomentumDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfSpecificAngularMomentum unit)
    {
        SpecificAngularMomentum quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.SpecificAngularMomentum.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSpecificAngularMomentumDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfSpecificAngularMomentum unit)
    {
        SpecificAngularMomentum quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.SpecificAngularMomentum.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        SpecificAngularMomentum quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        SpecificAngularMomentum quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpecificAngularMomentum quantity = SpecificAngularMomentum.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpecificAngularMomentum quantity = (SpecificAngularMomentum)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpecificAngularMomentum quantity = SpecificAngularMomentum.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpecificAngularMomentum quantity = (SpecificAngularMomentum)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
