#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SolidAngleTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        SolidAngle quantity = SolidAngle.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneSteradian_ShouldMatchUnitScale()
    {
        SolidAngle quantity = SolidAngle.OneSteradian;

        Assert.Equal(UnitOfSolidAngle.Steradian.SolidAngle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneSquareRadian_ShouldMatchUnitScale()
    {
        SolidAngle quantity = SolidAngle.OneSquareRadian;

        Assert.Equal(UnitOfSolidAngle.SquareRadian.SolidAngle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneSquareDegree_ShouldMatchUnitScale()
    {
        SolidAngle quantity = SolidAngle.OneSquareDegree;

        Assert.Equal(UnitOfSolidAngle.SquareDegree.SolidAngle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneSquareArcminute_ShouldMatchUnitScale()
    {
        SolidAngle quantity = SolidAngle.OneSquareArcminute;

        Assert.Equal(UnitOfSolidAngle.SquareArcminute.SolidAngle.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneSquareArcsecond_ShouldMatchUnitScale()
    {
        SolidAngle quantity = SolidAngle.OneSquareArcsecond;

        Assert.Equal(UnitOfSolidAngle.SquareArcsecond.SolidAngle.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSolidAngleDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfSolidAngle unit)
    {
        SolidAngle quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.SolidAngle.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSolidAngleDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfSolidAngle unit)
    {
        SolidAngle quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.SolidAngle.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        SolidAngle quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        SolidAngle quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SolidAngle quantity = SolidAngle.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SolidAngle quantity = (SolidAngle)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SolidAngle quantity = SolidAngle.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SolidAngle quantity = (SolidAngle)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
