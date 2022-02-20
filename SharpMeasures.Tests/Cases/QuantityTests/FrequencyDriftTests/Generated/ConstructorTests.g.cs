#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.FrequencyDriftTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        FrequencyDrift quantity = FrequencyDrift.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneHertzPerSecond_ShouldMatchUnitScale()
    {
        FrequencyDrift quantity = FrequencyDrift.OneHertzPerSecond;

        Assert.Equal(UnitOfFrequencyDrift.HertzPerSecond.FrequencyDrift.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePerSecondSquared_ShouldMatchUnitScale()
    {
        FrequencyDrift quantity = FrequencyDrift.OnePerSecondSquared;

        Assert.Equal(UnitOfFrequencyDrift.PerSecondSquared.FrequencyDrift.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfFrequencyDriftDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfFrequencyDrift unit)
    {
        FrequencyDrift quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.FrequencyDrift.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfFrequencyDriftDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfFrequencyDrift unit)
    {
        FrequencyDrift quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.FrequencyDrift.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        FrequencyDrift quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        FrequencyDrift quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        FrequencyDrift quantity = FrequencyDrift.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        FrequencyDrift quantity = (FrequencyDrift)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        FrequencyDrift quantity = FrequencyDrift.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        FrequencyDrift quantity = (FrequencyDrift)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
