#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.VolumeTests;

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
        Volume quantity = Volume.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneCubicMetre_ShouldMatchUnitScale()
    {
        Volume quantity = Volume.OneCubicMetre;

        Assert.Equal(UnitOfVolume.CubicMetre.Volume.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCubicDecimetre_ShouldMatchUnitScale()
    {
        Volume quantity = Volume.OneCubicDecimetre;

        Assert.Equal(UnitOfVolume.CubicDecimetre.Volume.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneLitre_ShouldMatchUnitScale()
    {
        Volume quantity = Volume.OneLitre;

        Assert.Equal(UnitOfVolume.Litre.Volume.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMillilitre_ShouldMatchUnitScale()
    {
        Volume quantity = Volume.OneMillilitre;

        Assert.Equal(UnitOfVolume.Millilitre.Volume.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCentilitre_ShouldMatchUnitScale()
    {
        Volume quantity = Volume.OneCentilitre;

        Assert.Equal(UnitOfVolume.Centilitre.Volume.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneDecilitre_ShouldMatchUnitScale()
    {
        Volume quantity = Volume.OneDecilitre;

        Assert.Equal(UnitOfVolume.Decilitre.Volume.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVolumeDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfVolume unit)
    {
        Volume quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Volume.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVolumeDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfVolume unit)
    {
        Volume quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Volume.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Volume quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Volume quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Volume quantity = Volume.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Volume quantity = (Volume)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Volume quantity = Volume.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Volume quantity = (Volume)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(LengthDataset))]
    public void FromLength_ShouldMatchExpression(Length sourceQuantity)
    {
        Volume quantity = Volume.From(sourceQuantity);

        Assert.Equal(Math.Pow(sourceQuantity.Magnitude, 3), quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DistanceDataset))]
    public void FromDistance_ShouldMatchExpression(Distance sourceQuantity)
    {
        Volume quantity = Volume.From(sourceQuantity);

        Assert.Equal(Math.Pow(sourceQuantity.Magnitude, 3), quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<LengthDataset, LengthDataset, LengthDataset>))]
    public void FromThreeLength_ShouldMatchExpression(Length sourceQuantity1, Length sourceQuantity2, Length sourceQuantity3)
    {
        Volume quantity = Volume.From(sourceQuantity1, sourceQuantity2, sourceQuantity3);

        Assert.Equal(sourceQuantity1.Magnitude * sourceQuantity2.Magnitude * sourceQuantity3.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<DistanceDataset, DistanceDataset, DistanceDataset>))]
    public void FromThreeDistance_ShouldMatchExpression(Distance sourceQuantity1, Distance sourceQuantity2, Distance sourceQuantity3)
    {
        Volume quantity = Volume.From(sourceQuantity1, sourceQuantity2, sourceQuantity3);

        Assert.Equal(sourceQuantity1.Magnitude * sourceQuantity2.Magnitude * sourceQuantity3.Magnitude, quantity.Magnitude, 2);
    }
}
