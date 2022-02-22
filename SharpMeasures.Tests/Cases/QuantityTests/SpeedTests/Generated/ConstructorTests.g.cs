#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Speed quantity = Speed.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneMetrePerSecond_ShouldMatchUnitScale()
    {
        Speed quantity = Speed.OneMetrePerSecond;

        Assert.Equal(UnitOfVelocity.MetrePerSecond.Speed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilometrePerSecond_ShouldMatchUnitScale()
    {
        Speed quantity = Speed.OneKilometrePerSecond;

        Assert.Equal(UnitOfVelocity.KilometrePerSecond.Speed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilometrePerHour_ShouldMatchUnitScale()
    {
        Speed quantity = Speed.OneKilometrePerHour;

        Assert.Equal(UnitOfVelocity.KilometrePerHour.Speed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFootPerSecond_ShouldMatchUnitScale()
    {
        Speed quantity = Speed.OneFootPerSecond;

        Assert.Equal(UnitOfVelocity.FootPerSecond.Speed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneYardPerSecond_ShouldMatchUnitScale()
    {
        Speed quantity = Speed.OneYardPerSecond;

        Assert.Equal(UnitOfVelocity.YardPerSecond.Speed.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMilePerHour_ShouldMatchUnitScale()
    {
        Speed quantity = Speed.OneMilePerHour;

        Assert.Equal(UnitOfVelocity.MilePerHour.Speed.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVelocityDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfVelocity unit)
    {
        Speed quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Speed.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVelocityDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfVelocity unit)
    {
        Speed quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Speed.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Speed quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Speed quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Speed quantity = Speed.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Speed quantity = (Speed)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Speed quantity = Speed.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Speed quantity = (Speed)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
