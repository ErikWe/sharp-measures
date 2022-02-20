#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TimeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Time quantity = Time.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneSecond_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneSecond;

        Assert.Equal(UnitOfTime.Second.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMinute_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneMinute;

        Assert.Equal(UnitOfTime.Minute.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneHour_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneHour;

        Assert.Equal(UnitOfTime.Hour.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneDay_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneDay;

        Assert.Equal(UnitOfTime.Day.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneWeek_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneWeek;

        Assert.Equal(UnitOfTime.Week.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCommonYear_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneCommonYear;

        Assert.Equal(UnitOfTime.CommonYear.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneJulianYear_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneJulianYear;

        Assert.Equal(UnitOfTime.JulianYear.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFemtosecond_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneFemtosecond;

        Assert.Equal(UnitOfTime.Femtosecond.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePicosecond_ShouldMatchUnitScale()
    {
        Time quantity = Time.OnePicosecond;

        Assert.Equal(UnitOfTime.Picosecond.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneNanosecond_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneNanosecond;

        Assert.Equal(UnitOfTime.Nanosecond.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMicrosecond_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneMicrosecond;

        Assert.Equal(UnitOfTime.Microsecond.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMillisecond_ShouldMatchUnitScale()
    {
        Time quantity = Time.OneMillisecond;

        Assert.Equal(UnitOfTime.Millisecond.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTimeDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfTime unit)
    {
        Time quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTimeDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfTime unit)
    {
        Time quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Time.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Time quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Time quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Time quantity = Time.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Time quantity = (Time)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Time quantity = Time.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Time quantity = (Time)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
