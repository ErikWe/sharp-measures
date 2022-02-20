#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PotentialEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        PotentialEnergy quantity = PotentialEnergy.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneJoule_ShouldMatchUnitScale()
    {
        PotentialEnergy quantity = PotentialEnergy.OneJoule;

        Assert.Equal(UnitOfEnergy.Joule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilojoule_ShouldMatchUnitScale()
    {
        PotentialEnergy quantity = PotentialEnergy.OneKilojoule;

        Assert.Equal(UnitOfEnergy.Kilojoule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMegajoule_ShouldMatchUnitScale()
    {
        PotentialEnergy quantity = PotentialEnergy.OneMegajoule;

        Assert.Equal(UnitOfEnergy.Megajoule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneGigajoule_ShouldMatchUnitScale()
    {
        PotentialEnergy quantity = PotentialEnergy.OneGigajoule;

        Assert.Equal(UnitOfEnergy.Gigajoule.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilowattHour_ShouldMatchUnitScale()
    {
        PotentialEnergy quantity = PotentialEnergy.OneKilowattHour;

        Assert.Equal(UnitOfEnergy.KilowattHour.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCalorie_ShouldMatchUnitScale()
    {
        PotentialEnergy quantity = PotentialEnergy.OneCalorie;

        Assert.Equal(UnitOfEnergy.Calorie.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilocalorie_ShouldMatchUnitScale()
    {
        PotentialEnergy quantity = PotentialEnergy.OneKilocalorie;

        Assert.Equal(UnitOfEnergy.Kilocalorie.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfEnergyDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfEnergy unit)
    {
        PotentialEnergy quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfEnergyDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfEnergy unit)
    {
        PotentialEnergy quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Energy.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        PotentialEnergy quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        PotentialEnergy quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        PotentialEnergy quantity = PotentialEnergy.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        PotentialEnergy quantity = (PotentialEnergy)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        PotentialEnergy quantity = PotentialEnergy.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        PotentialEnergy quantity = (PotentialEnergy)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
