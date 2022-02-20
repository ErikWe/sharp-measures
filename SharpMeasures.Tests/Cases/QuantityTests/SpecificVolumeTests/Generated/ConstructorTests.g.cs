#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificVolumeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        SpecificVolume quantity = SpecificVolume.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneCubicMetrePerKilogram_ShouldMatchUnitScale()
    {
        SpecificVolume quantity = SpecificVolume.OneCubicMetrePerKilogram;

        Assert.Equal(UnitOfSpecificVolume.CubicMetrePerKilogram.SpecificVolume.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSpecificVolumeDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfSpecificVolume unit)
    {
        SpecificVolume quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.SpecificVolume.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSpecificVolumeDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfSpecificVolume unit)
    {
        SpecificVolume quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.SpecificVolume.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        SpecificVolume quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        SpecificVolume quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpecificVolume quantity = SpecificVolume.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpecificVolume quantity = (SpecificVolume)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpecificVolume quantity = SpecificVolume.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpecificVolume quantity = (SpecificVolume)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
