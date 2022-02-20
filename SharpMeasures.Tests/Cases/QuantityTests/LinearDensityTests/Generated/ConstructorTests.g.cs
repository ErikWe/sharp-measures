#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.LinearDensityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        LinearDensity quantity = LinearDensity.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneKilogramPerMetre_ShouldMatchUnitScale()
    {
        LinearDensity quantity = LinearDensity.OneKilogramPerMetre;

        Assert.Equal(UnitOfLinearDensity.KilogramPerMetre.LinearDensity.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfLinearDensityDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfLinearDensity unit)
    {
        LinearDensity quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.LinearDensity.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfLinearDensityDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfLinearDensity unit)
    {
        LinearDensity quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.LinearDensity.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        LinearDensity quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        LinearDensity quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        LinearDensity quantity = LinearDensity.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        LinearDensity quantity = (LinearDensity)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        LinearDensity quantity = LinearDensity.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        LinearDensity quantity = (LinearDensity)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
