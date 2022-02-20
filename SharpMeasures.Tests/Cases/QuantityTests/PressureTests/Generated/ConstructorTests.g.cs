#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PressureTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Pressure quantity = Pressure.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OnePascal_ShouldMatchUnitScale()
    {
        Pressure quantity = Pressure.OnePascal;

        Assert.Equal(UnitOfPressure.Pascal.Pressure.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilopascal_ShouldMatchUnitScale()
    {
        Pressure quantity = Pressure.OneKilopascal;

        Assert.Equal(UnitOfPressure.Kilopascal.Pressure.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneBar_ShouldMatchUnitScale()
    {
        Pressure quantity = Pressure.OneBar;

        Assert.Equal(UnitOfPressure.Bar.Pressure.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneStandardAtmosphere_ShouldMatchUnitScale()
    {
        Pressure quantity = Pressure.OneStandardAtmosphere;

        Assert.Equal(UnitOfPressure.StandardAtmosphere.Pressure.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePoundForcePerSquareInch_ShouldMatchUnitScale()
    {
        Pressure quantity = Pressure.OnePoundForcePerSquareInch;

        Assert.Equal(UnitOfPressure.PoundForcePerSquareInch.Pressure.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfPressureDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfPressure unit)
    {
        Pressure quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Pressure.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfPressureDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfPressure unit)
    {
        Pressure quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Pressure.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Pressure quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Pressure quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Pressure quantity = Pressure.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Pressure quantity = (Pressure)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Pressure quantity = Pressure.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Pressure quantity = (Pressure)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
