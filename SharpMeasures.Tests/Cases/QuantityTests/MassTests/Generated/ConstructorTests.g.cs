#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MassTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Mass quantity = Mass.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneGram_ShouldMatchUnitScale()
    {
        Mass quantity = Mass.OneGram;

        Assert.Equal(UnitOfMass.Gram.Mass.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMilligram_ShouldMatchUnitScale()
    {
        Mass quantity = Mass.OneMilligram;

        Assert.Equal(UnitOfMass.Milligram.Mass.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneHectogram_ShouldMatchUnitScale()
    {
        Mass quantity = Mass.OneHectogram;

        Assert.Equal(UnitOfMass.Hectogram.Mass.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilogram_ShouldMatchUnitScale()
    {
        Mass quantity = Mass.OneKilogram;

        Assert.Equal(UnitOfMass.Kilogram.Mass.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneTonne_ShouldMatchUnitScale()
    {
        Mass quantity = Mass.OneTonne;

        Assert.Equal(UnitOfMass.Tonne.Mass.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneOunce_ShouldMatchUnitScale()
    {
        Mass quantity = Mass.OneOunce;

        Assert.Equal(UnitOfMass.Ounce.Mass.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePound_ShouldMatchUnitScale()
    {
        Mass quantity = Mass.OnePound;

        Assert.Equal(UnitOfMass.Pound.Mass.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMassDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfMass unit)
    {
        Mass quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Mass.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMassDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfMass unit)
    {
        Mass quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Mass.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Mass quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Mass quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Mass quantity = Mass.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Mass quantity = (Mass)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Mass quantity = Mass.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Mass quantity = (Mass)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
