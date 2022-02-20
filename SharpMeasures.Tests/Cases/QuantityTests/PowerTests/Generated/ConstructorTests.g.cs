#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PowerTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Power quantity = Power.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneWatt_ShouldMatchUnitScale()
    {
        Power quantity = Power.OneWatt;

        Assert.Equal(UnitOfPower.Watt.Power.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilowatt_ShouldMatchUnitScale()
    {
        Power quantity = Power.OneKilowatt;

        Assert.Equal(UnitOfPower.Kilowatt.Power.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMegawatt_ShouldMatchUnitScale()
    {
        Power quantity = Power.OneMegawatt;

        Assert.Equal(UnitOfPower.Megawatt.Power.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneGigawatt_ShouldMatchUnitScale()
    {
        Power quantity = Power.OneGigawatt;

        Assert.Equal(UnitOfPower.Gigawatt.Power.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneTerawatt_ShouldMatchUnitScale()
    {
        Power quantity = Power.OneTerawatt;

        Assert.Equal(UnitOfPower.Terawatt.Power.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfPowerDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfPower unit)
    {
        Power quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Power.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfPowerDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfPower unit)
    {
        Power quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Power.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Power quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Power quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Power quantity = Power.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Power quantity = (Power)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Power quantity = Power.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Power quantity = (Power)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
