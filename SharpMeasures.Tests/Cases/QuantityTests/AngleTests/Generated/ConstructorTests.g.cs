#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngleTests;

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
        Angle quantity = Angle.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneRadian_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneRadian;

        Assert.Equal(UnitOfAngle.Radian.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMilliradian_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneMilliradian;

        Assert.Equal(UnitOfAngle.Milliradian.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneDegree_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneDegree;

        Assert.Equal(UnitOfAngle.Degree.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneGradian_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneGradian;

        Assert.Equal(UnitOfAngle.Gradian.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneArcminute_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneArcminute;

        Assert.Equal(UnitOfAngle.Arcminute.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneArcsecond_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneArcsecond;

        Assert.Equal(UnitOfAngle.Arcsecond.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMilliarcsecond_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneMilliarcsecond;

        Assert.Equal(UnitOfAngle.Milliarcsecond.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMicroarcsecond_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneMicroarcsecond;

        Assert.Equal(UnitOfAngle.Microarcsecond.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneTurn_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneTurn;

        Assert.Equal(UnitOfAngle.Turn.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneHalfTurn_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneHalfTurn;

        Assert.Equal(UnitOfAngle.HalfTurn.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneQuarterTurn_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneQuarterTurn;

        Assert.Equal(UnitOfAngle.QuarterTurn.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCentiturn_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneCentiturn;

        Assert.Equal(UnitOfAngle.Centiturn.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMilliturn_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneMilliturn;

        Assert.Equal(UnitOfAngle.Milliturn.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneBinaryDegree_ShouldMatchUnitScale()
    {
        Angle quantity = Angle.OneBinaryDegree;

        Assert.Equal(UnitOfAngle.BinaryDegree.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngleDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfAngle unit)
    {
        Angle quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngleDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfAngle unit)
    {
        Angle quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Angle.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Angle quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Angle quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Angle quantity = Angle.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Angle quantity = (Angle)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Angle quantity = Angle.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Angle quantity = (Angle)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(SolidAngleDataset))]
    public void FromSolidAngle_ShouldMatchExpression(SolidAngle sourceQuantity)
    {
        Angle quantity = Angle.From(sourceQuantity);

        Assert.Equal(Math.Sqrt(sourceQuantity.Magnitude), quantity.Magnitude, 2);
    }
}
