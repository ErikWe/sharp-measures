#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.FrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using System;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Frequency quantity = Frequency.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OnePerSecond_ShouldMatchUnitScale()
    {
        Frequency quantity = Frequency.OnePerSecond;

        Assert.Equal(UnitOfFrequency.PerSecond.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePerMinute_ShouldMatchUnitScale()
    {
        Frequency quantity = Frequency.OnePerMinute;

        Assert.Equal(UnitOfFrequency.PerMinute.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePerHour_ShouldMatchUnitScale()
    {
        Frequency quantity = Frequency.OnePerHour;

        Assert.Equal(UnitOfFrequency.PerHour.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneHertz_ShouldMatchUnitScale()
    {
        Frequency quantity = Frequency.OneHertz;

        Assert.Equal(UnitOfFrequency.Hertz.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilohertz_ShouldMatchUnitScale()
    {
        Frequency quantity = Frequency.OneKilohertz;

        Assert.Equal(UnitOfFrequency.Kilohertz.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMegahertz_ShouldMatchUnitScale()
    {
        Frequency quantity = Frequency.OneMegahertz;

        Assert.Equal(UnitOfFrequency.Megahertz.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneGigahertz_ShouldMatchUnitScale()
    {
        Frequency quantity = Frequency.OneGigahertz;

        Assert.Equal(UnitOfFrequency.Gigahertz.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneTerahertz_ShouldMatchUnitScale()
    {
        Frequency quantity = Frequency.OneTerahertz;

        Assert.Equal(UnitOfFrequency.Terahertz.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfFrequencyDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfFrequency unit)
    {
        Frequency quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfFrequencyDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfFrequency unit)
    {
        Frequency quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Frequency.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Frequency quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Frequency quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Frequency quantity = Frequency.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Frequency quantity = (Frequency)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Frequency quantity = Frequency.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Frequency quantity = (Frequency)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(TimeDataset))]
    public void FromTime_ShouldMatchExpression(Time sourceQuantity)
    {
        Frequency quantity = Frequency.From(sourceQuantity);

        Assert.Equal(1 / sourceQuantity.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(FrequencyDriftDataset))]
    public void FromFrequencyDrift_ShouldMatchExpression(FrequencyDrift sourceQuantity)
    {
        Frequency quantity = Frequency.From(sourceQuantity);

        Assert.Equal(Math.Sqrt(sourceQuantity.Magnitude), quantity.Magnitude, 2);
    }
}
