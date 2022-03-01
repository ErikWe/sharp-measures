#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpeedSquaredTests;

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
        SpeedSquared quantity = SpeedSquared.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneSquareMetrePerSecondSquared_ShouldMatchUnitScale()
    {
        SpeedSquared quantity = SpeedSquared.OneSquareMetrePerSecondSquared;

        Assert.Equal(UnitOfVelocitySquared.SquareMetrePerSecondSquared.SpeedSquared.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVelocitySquaredDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfVelocitySquared unit)
    {
        SpeedSquared quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.SpeedSquared.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVelocitySquaredDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfVelocitySquared unit)
    {
        SpeedSquared quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.SpeedSquared.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        SpeedSquared quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        SpeedSquared quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpeedSquared quantity = SpeedSquared.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpeedSquared quantity = (SpeedSquared)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpeedSquared quantity = SpeedSquared.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpeedSquared quantity = (SpeedSquared)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(SpeedDataset))]
    public void FromSpeed_ShouldMatchExpression(Speed sourceQuantity)
    {
        SpeedSquared quantity = SpeedSquared.From(sourceQuantity);

        Assert.Equal(Math.Pow(sourceQuantity.Magnitude, 2), quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpeedDataset, SpeedDataset>))]
    public void FromTwoSpeed_ShouldMatchExpression(Speed sourceQuantity1, Speed sourceQuantity2)
    {
        SpeedSquared quantity = SpeedSquared.From(sourceQuantity1, sourceQuantity2);

        Assert.Equal(sourceQuantity1.Magnitude * sourceQuantity2.Magnitude, quantity.Magnitude, 2);
    }
}
