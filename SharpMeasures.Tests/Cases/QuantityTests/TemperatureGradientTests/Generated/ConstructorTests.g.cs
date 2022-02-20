#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureGradientTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        TemperatureGradient quantity = TemperatureGradient.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneKelvinPerMetre_ShouldMatchUnitScale()
    {
        TemperatureGradient quantity = TemperatureGradient.OneKelvinPerMetre;

        Assert.Equal(UnitOfTemperatureGradient.KelvinPerMetre.TemperatureGradient.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCelsiusPerMetre_ShouldMatchUnitScale()
    {
        TemperatureGradient quantity = TemperatureGradient.OneCelsiusPerMetre;

        Assert.Equal(UnitOfTemperatureGradient.CelsiusPerMetre.TemperatureGradient.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneRankinePerMetre_ShouldMatchUnitScale()
    {
        TemperatureGradient quantity = TemperatureGradient.OneRankinePerMetre;

        Assert.Equal(UnitOfTemperatureGradient.RankinePerMetre.TemperatureGradient.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFahrenheitPerMetre_ShouldMatchUnitScale()
    {
        TemperatureGradient quantity = TemperatureGradient.OneFahrenheitPerMetre;

        Assert.Equal(UnitOfTemperatureGradient.FahrenheitPerMetre.TemperatureGradient.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFahrenheitPerFoot_ShouldMatchUnitScale()
    {
        TemperatureGradient quantity = TemperatureGradient.OneFahrenheitPerFoot;

        Assert.Equal(UnitOfTemperatureGradient.FahrenheitPerFoot.TemperatureGradient.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTemperatureGradientDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfTemperatureGradient unit)
    {
        TemperatureGradient quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.TemperatureGradient.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTemperatureGradientDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfTemperatureGradient unit)
    {
        TemperatureGradient quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.TemperatureGradient.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        TemperatureGradient quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        TemperatureGradient quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        TemperatureGradient quantity = TemperatureGradient.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        TemperatureGradient quantity = (TemperatureGradient)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        TemperatureGradient quantity = TemperatureGradient.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        TemperatureGradient quantity = (TemperatureGradient)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
