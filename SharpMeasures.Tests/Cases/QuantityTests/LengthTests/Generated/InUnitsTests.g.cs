#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.LengthTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfLengthDataset>))]
    public void InUnit(Scalar expected, UnitOfLength unit)
    {
        Length quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMetre(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Metre);

        Scalar actual = quantity.Metres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFemtometre(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Femtometre);

        Scalar actual = quantity.Femtometres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPicometre(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Picometre);

        Scalar actual = quantity.Picometres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InNanometre(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Nanometre);

        Scalar actual = quantity.Nanometres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMicrometre(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Micrometre);

        Scalar actual = quantity.Micrometres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMillimetre(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Millimetre);

        Scalar actual = quantity.Millimetres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCentimetre(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Centimetre);

        Scalar actual = quantity.Centimetres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InDecimetre(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Decimetre);

        Scalar actual = quantity.Decimetres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilometre(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Kilometre);

        Scalar actual = quantity.Kilometres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InAstronomicalUnit(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.AstronomicalUnit);

        Scalar actual = quantity.AstronomicalUnits;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InLightYear(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.LightYear);

        Scalar actual = quantity.LightYears;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InParsec(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Parsec);

        Scalar actual = quantity.Parsecs;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKiloparsec(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Kiloparsec);

        Scalar actual = quantity.Kiloparsecs;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMegaparsec(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Megaparsec);

        Scalar actual = quantity.Megaparsecs;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InGigaparsec(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Gigaparsec);

        Scalar actual = quantity.Gigaparsecs;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InInch(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Inch);

        Scalar actual = quantity.Inches;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFoot(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Foot);

        Scalar actual = quantity.Feet;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InYard(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Yard);

        Scalar actual = quantity.Yards;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMile(Scalar expected)
    {
        Length quantity = new(expected, UnitOfLength.Mile);

        Scalar actual = quantity.Miles;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
