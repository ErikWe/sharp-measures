#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.JerkTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Jerk quantity = Jerk.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneMetrePerSecondCubed_ShouldMatchUnitScale()
    {
        Jerk quantity = Jerk.OneMetrePerSecondCubed;

        Assert.Equal(UnitOfJerk.MetrePerSecondCubed.Jerk.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFootPerSecondCubed_ShouldMatchUnitScale()
    {
        Jerk quantity = Jerk.OneFootPerSecondCubed;

        Assert.Equal(UnitOfJerk.FootPerSecondCubed.Jerk.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfJerkDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfJerk unit)
    {
        Jerk quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Jerk.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfJerkDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfJerk unit)
    {
        Jerk quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Jerk.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Jerk quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Jerk quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Jerk quantity = Jerk.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Jerk quantity = (Jerk)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Jerk quantity = Jerk.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Jerk quantity = (Jerk)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
