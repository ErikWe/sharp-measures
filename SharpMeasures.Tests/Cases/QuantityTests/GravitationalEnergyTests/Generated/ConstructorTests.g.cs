#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.GravitationalEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        GravitationalEnergy quantity = GravitationalEnergy.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneJoule_ShouldMatchUnitScale()
    {
        GravitationalEnergy quantity = GravitationalEnergy.OneJoule;

        Assert.Equal(UnitOfEnergy.Joule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilojoule_ShouldMatchUnitScale()
    {
        GravitationalEnergy quantity = GravitationalEnergy.OneKilojoule;

        Assert.Equal(UnitOfEnergy.Kilojoule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMegajoule_ShouldMatchUnitScale()
    {
        GravitationalEnergy quantity = GravitationalEnergy.OneMegajoule;

        Assert.Equal(UnitOfEnergy.Megajoule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneGigajoule_ShouldMatchUnitScale()
    {
        GravitationalEnergy quantity = GravitationalEnergy.OneGigajoule;

        Assert.Equal(UnitOfEnergy.Gigajoule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilowattHour_ShouldMatchUnitScale()
    {
        GravitationalEnergy quantity = GravitationalEnergy.OneKilowattHour;

        Assert.Equal(UnitOfEnergy.KilowattHour.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCalorie_ShouldMatchUnitScale()
    {
        GravitationalEnergy quantity = GravitationalEnergy.OneCalorie;

        Assert.Equal(UnitOfEnergy.Calorie.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilocalorie_ShouldMatchUnitScale()
    {
        GravitationalEnergy quantity = GravitationalEnergy.OneKilocalorie;

        Assert.Equal(UnitOfEnergy.Kilocalorie.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfEnergyDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfEnergy unit)
    {
        GravitationalEnergy quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfEnergyDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfEnergy unit)
    {
        GravitationalEnergy quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        GravitationalEnergy quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        GravitationalEnergy quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        GravitationalEnergy quantity = GravitationalEnergy.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        GravitationalEnergy quantity = (GravitationalEnergy)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        GravitationalEnergy quantity = GravitationalEnergy.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        GravitationalEnergy quantity = (GravitationalEnergy)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

}
