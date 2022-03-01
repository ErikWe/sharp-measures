#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureDifferenceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void OneKelvin_ShouldMatchUnitScale()
    {
        TemperatureDifference quantity = TemperatureDifference.OneKelvin;

        Assert.Equal(UnitOfTemperature.Kelvin.TemperatureDifference.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCelsius_ShouldMatchUnitScale()
    {
        TemperatureDifference quantity = TemperatureDifference.OneCelsius;

        Assert.Equal(UnitOfTemperature.Celsius.TemperatureDifference.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneRankine_ShouldMatchUnitScale()
    {
        TemperatureDifference quantity = TemperatureDifference.OneRankine;

        Assert.Equal(UnitOfTemperature.Rankine.TemperatureDifference.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFahrenheit_ShouldMatchUnitScale()
    {
        TemperatureDifference quantity = TemperatureDifference.OneFahrenheit;

        Assert.Equal(UnitOfTemperature.Fahrenheit.TemperatureDifference.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTemperatureDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfTemperature unit)
    {
        TemperatureDifference quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.TemperatureDifference.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTemperatureDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfTemperature unit)
    {
        TemperatureDifference quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.TemperatureDifference.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        TemperatureDifference quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        TemperatureDifference quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        TemperatureDifference quantity = TemperatureDifference.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        TemperatureDifference quantity = (TemperatureDifference)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        TemperatureDifference quantity = TemperatureDifference.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        TemperatureDifference quantity = (TemperatureDifference)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

}
