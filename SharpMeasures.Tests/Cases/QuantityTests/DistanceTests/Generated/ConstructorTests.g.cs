#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.DistanceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Fact]
    public void Zero_MagnitudeShouldBeZero()
    {
        Distance quantity = Distance.Zero;

        Assert.Equal(0, quantity.Magnitude);
    }

    [Fact]
    public void OneMetre_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneMetre;

        Assert.Equal(UnitOfLength.Metre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFemtometre_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneFemtometre;

        Assert.Equal(UnitOfLength.Femtometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OnePicometre_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OnePicometre;

        Assert.Equal(UnitOfLength.Picometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneNanometre_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneNanometre;

        Assert.Equal(UnitOfLength.Nanometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMicrometre_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneMicrometre;

        Assert.Equal(UnitOfLength.Micrometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMillimetre_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneMillimetre;

        Assert.Equal(UnitOfLength.Millimetre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneCentimetre_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneCentimetre;

        Assert.Equal(UnitOfLength.Centimetre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneDecimetre_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneDecimetre;

        Assert.Equal(UnitOfLength.Decimetre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKilometre_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneKilometre;

        Assert.Equal(UnitOfLength.Kilometre.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneAstronomicalUnit_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneAstronomicalUnit;

        Assert.Equal(UnitOfLength.AstronomicalUnit.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneLightYear_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneLightYear;

        Assert.Equal(UnitOfLength.LightYear.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneParsec_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneParsec;

        Assert.Equal(UnitOfLength.Parsec.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneKiloparsec_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneKiloparsec;

        Assert.Equal(UnitOfLength.Kiloparsec.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMegaparsec_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneMegaparsec;

        Assert.Equal(UnitOfLength.Megaparsec.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneGigaparsec_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneGigaparsec;

        Assert.Equal(UnitOfLength.Gigaparsec.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneInch_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneInch;

        Assert.Equal(UnitOfLength.Inch.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneFoot_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneFoot;

        Assert.Equal(UnitOfLength.Foot.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneYard_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneYard;

        Assert.Equal(UnitOfLength.Yard.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Fact]
    public void OneMile_ShouldMatchUnitScale()
    {
        Distance quantity = Distance.OneMile;

        Assert.Equal(UnitOfLength.Mile.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfLengthDataset>))]
    public void Scalar_Unit_MagnitudeShouldBeMultipliedByUnitScale(Scalar magnitude, UnitOfLength unit)
    {
        Distance quantity = new(magnitude, unit);
        
        Assert.Equal(magnitude * unit.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfLengthDataset>))]
    public void Double_Unit_MagnitudeShouldBeMultipliedByUnitScale(double magnitude, UnitOfLength unit)
    {
        Distance quantity = new(magnitude, unit);

        Assert.Equal(magnitude * unit.Length.Magnitude, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Scalar_MagnitudeShouldBeEqual(Scalar magnitude)
    {
        Distance quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void Double_MagnitudeShouldBeEqual(double magnitude)
    {
        Distance quantity = new(magnitude);

        Assert.Equal(magnitude, quantity.Magnitude);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Distance quantity = Distance.FromDouble(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastedDouble_MagnitudeShouldBeEqualToDouble(double a)
    {
        Distance quantity = (Distance)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Distance quantity = Distance.FromScalar(a);

        Assert.Equal(a, quantity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastedScalar_MagnitudesShouldBeEqual(Scalar a)
    {
        Distance quantity = (Distance)a;

        Assert.Equal(a, quantity.Magnitude, 2);
    }
}
