#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.LengthTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Length quantity = Length.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneMetre_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneMetre;

        Assert.Equal(UnitOfLength.Metre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFemtometre_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneFemtometre;

        Assert.Equal(UnitOfLength.Femtometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePicometre_ShouldMatchUnitScale()
    {
        Length quantity = Length.OnePicometre;

        Assert.Equal(UnitOfLength.Picometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneNanometre_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneNanometre;

        Assert.Equal(UnitOfLength.Nanometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMicrometre_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneMicrometre;

        Assert.Equal(UnitOfLength.Micrometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMillimetre_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneMillimetre;

        Assert.Equal(UnitOfLength.Millimetre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCentimetre_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneCentimetre;

        Assert.Equal(UnitOfLength.Centimetre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneDecimetre_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneDecimetre;

        Assert.Equal(UnitOfLength.Decimetre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilometre_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneKilometre;

        Assert.Equal(UnitOfLength.Kilometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneAstronomicalUnit_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneAstronomicalUnit;

        Assert.Equal(UnitOfLength.AstronomicalUnit.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneLightYear_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneLightYear;

        Assert.Equal(UnitOfLength.LightYear.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneParsec_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneParsec;

        Assert.Equal(UnitOfLength.Parsec.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKiloparsec_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneKiloparsec;

        Assert.Equal(UnitOfLength.Kiloparsec.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMegaparsec_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneMegaparsec;

        Assert.Equal(UnitOfLength.Megaparsec.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneGigaparsec_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneGigaparsec;

        Assert.Equal(UnitOfLength.Gigaparsec.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneInch_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneInch;

        Assert.Equal(UnitOfLength.Inch.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFoot_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneFoot;

        Assert.Equal(UnitOfLength.Foot.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneYard_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneYard;

        Assert.Equal(UnitOfLength.Yard.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMile_ShouldMatchUnitScale()
    {
        Length quantity = Length.OneMile;

        Assert.Equal(UnitOfLength.Mile.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfLengthDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfLength unit)
    {
        Length quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfLengthDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfLength unit)
    {
        Length quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Length quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Length quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Length quantity = Length.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Length quantity = (Length)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Length quantity = Length.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Length quantity = (Length)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
