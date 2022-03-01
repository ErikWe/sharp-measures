#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TimeSquaredTests;

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
        TimeSquared quantity = TimeSquared.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneSquareSecond_ShouldMatchUnitScale()
    {
        TimeSquared quantity = TimeSquared.OneSquareSecond;

        Assert.Equal(UnitOfTimeSquared.SquareSecond.TimeSquared.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTimeSquaredDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfTimeSquared unit)
    {
        TimeSquared quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.TimeSquared.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTimeSquaredDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfTimeSquared unit)
    {
        TimeSquared quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.TimeSquared.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        TimeSquared quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        TimeSquared quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        TimeSquared quantity = TimeSquared.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        TimeSquared quantity = (TimeSquared)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        TimeSquared quantity = TimeSquared.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        TimeSquared quantity = (TimeSquared)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(FrequencyDriftDataset))]
    public void FromFrequencyDrift_ShouldMatchExpression(FrequencyDrift sourceQuantity)
    {
        TimeSquared quantity = TimeSquared.From(sourceQuantity);

        Assert.Equal(1 / sourceQuantity.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(TimeDataset))]
    public void FromTime_ShouldMatchExpression(Time sourceQuantity)
    {
        TimeSquared quantity = TimeSquared.From(sourceQuantity);

        Assert.Equal(Math.Pow(sourceQuantity.Magnitude, 2), quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeDataset, TimeDataset>))]
    public void FromTwoTime_ShouldMatchExpression(Time sourceQuantity1, Time sourceQuantity2)
    {
        TimeSquared quantity = TimeSquared.From(sourceQuantity1, sourceQuantity2);

        Assert.Equal(sourceQuantity1.Magnitude * sourceQuantity2.Magnitude, quantity.Magnitude, 2);
    }
}
