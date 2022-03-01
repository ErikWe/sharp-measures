#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.EnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Energy quantity = Energy.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneJoule_ShouldMatchUnitScale()
    {
        Energy quantity = Energy.OneJoule;

        Assert.Equal(UnitOfEnergy.Joule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilojoule_ShouldMatchUnitScale()
    {
        Energy quantity = Energy.OneKilojoule;

        Assert.Equal(UnitOfEnergy.Kilojoule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMegajoule_ShouldMatchUnitScale()
    {
        Energy quantity = Energy.OneMegajoule;

        Assert.Equal(UnitOfEnergy.Megajoule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneGigajoule_ShouldMatchUnitScale()
    {
        Energy quantity = Energy.OneGigajoule;

        Assert.Equal(UnitOfEnergy.Gigajoule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilowattHour_ShouldMatchUnitScale()
    {
        Energy quantity = Energy.OneKilowattHour;

        Assert.Equal(UnitOfEnergy.KilowattHour.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCalorie_ShouldMatchUnitScale()
    {
        Energy quantity = Energy.OneCalorie;

        Assert.Equal(UnitOfEnergy.Calorie.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilocalorie_ShouldMatchUnitScale()
    {
        Energy quantity = Energy.OneKilocalorie;

        Assert.Equal(UnitOfEnergy.Kilocalorie.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfEnergyDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfEnergy unit)
    {
        Energy quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfEnergyDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfEnergy unit)
    {
        Energy quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Energy quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Energy quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Energy quantity = Energy.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Energy quantity = (Energy)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Energy quantity = Energy.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Energy quantity = (Energy)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

}
