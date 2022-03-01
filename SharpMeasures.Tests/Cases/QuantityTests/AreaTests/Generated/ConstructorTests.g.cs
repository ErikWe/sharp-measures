#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AreaTests;

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
        Area quantity = Area.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneSquareMetre_ShouldMatchUnitScale()
    {
        Area quantity = Area.OneSquareMetre;

        Assert.Equal(UnitOfArea.SquareMetre.Area.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneSquareKilometre_ShouldMatchUnitScale()
    {
        Area quantity = Area.OneSquareKilometre;

        Assert.Equal(UnitOfArea.SquareKilometre.Area.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneSquareInch_ShouldMatchUnitScale()
    {
        Area quantity = Area.OneSquareInch;

        Assert.Equal(UnitOfArea.SquareInch.Area.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneSquareMile_ShouldMatchUnitScale()
    {
        Area quantity = Area.OneSquareMile;

        Assert.Equal(UnitOfArea.SquareMile.Area.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneAre_ShouldMatchUnitScale()
    {
        Area quantity = Area.OneAre;

        Assert.Equal(UnitOfArea.Are.Area.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneHectare_ShouldMatchUnitScale()
    {
        Area quantity = Area.OneHectare;

        Assert.Equal(UnitOfArea.Hectare.Area.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneAcre_ShouldMatchUnitScale()
    {
        Area quantity = Area.OneAcre;

        Assert.Equal(UnitOfArea.Acre.Area.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAreaDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfArea unit)
    {
        Area quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Area.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAreaDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfArea unit)
    {
        Area quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Area.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Area quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Area quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Area quantity = Area.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Area quantity = (Area)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Area quantity = Area.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Area quantity = (Area)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(LengthDataset))]
    public void FromLength_ShouldMatchExpression(Length sourceQuantity)
    {
        Area quantity = Area.From(sourceQuantity);

        Assert.Equal(Math.Pow(sourceQuantity.Magnitude, 2), quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void FromDistance_ShouldMatchExpression(Distance sourceQuantity)
    {
        Area quantity = Area.From(sourceQuantity);

        Assert.Equal(Math.Pow(sourceQuantity.Magnitude, 2), quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<LengthDataset, LengthDataset>))]
    public void FromTwoLength_ShouldMatchExpression(Length sourceQuantity1, Length sourceQuantity2)
    {
        Area quantity = Area.From(sourceQuantity1, sourceQuantity2);

        Assert.Equal(sourceQuantity1.Magnitude * sourceQuantity2.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DistanceDataset, DistanceDataset>))]
    public void FromTwoDistance_ShouldMatchExpression(Distance sourceQuantity1, Distance sourceQuantity2)
    {
        Area quantity = Area.From(sourceQuantity1, sourceQuantity2);

        Assert.Equal(sourceQuantity1.Magnitude * sourceQuantity2.Magnitude, quantity.Magnitude, 2);
    }
}
