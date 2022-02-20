#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpatialFrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        SpatialFrequency quantity = SpatialFrequency.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OnePerMetre_ShouldMatchUnitScale()
    {
        SpatialFrequency quantity = SpatialFrequency.OnePerMetre;

        Assert.Equal(UnitOfSpatialFrequency.PerMetre.SpatialFrequency.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSpatialFrequencyDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfSpatialFrequency unit)
    {
        SpatialFrequency quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.SpatialFrequency.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSpatialFrequencyDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfSpatialFrequency unit)
    {
        SpatialFrequency quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.SpatialFrequency.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        SpatialFrequency quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        SpatialFrequency quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpatialFrequency quantity = SpatialFrequency.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        SpatialFrequency quantity = (SpatialFrequency)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpatialFrequency quantity = SpatialFrequency.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        SpatialFrequency quantity = (SpatialFrequency)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
