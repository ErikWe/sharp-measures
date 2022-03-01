#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Displacement3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfLengthDataset>))]
    public void InUnit(Vector3 expected, UnitOfLength unit)
    {
        Displacement3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMetre(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Metre);

        Vector3 actual = quantity.Metres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InFemtometre(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Femtometre);

        Vector3 actual = quantity.Femtometres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InPicometre(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Picometre);

        Vector3 actual = quantity.Picometres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InNanometre(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Nanometre);

        Vector3 actual = quantity.Nanometres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMicrometre(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Micrometre);

        Vector3 actual = quantity.Micrometres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMillimetre(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Millimetre);

        Vector3 actual = quantity.Millimetres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InCentimetre(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Centimetre);

        Vector3 actual = quantity.Centimetres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InDecimetre(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Decimetre);

        Vector3 actual = quantity.Decimetres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InKilometre(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Kilometre);

        Vector3 actual = quantity.Kilometres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InAstronomicalUnit(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.AstronomicalUnit);

        Vector3 actual = quantity.AstronomicalUnits;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InLightYear(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.LightYear);

        Vector3 actual = quantity.LightYears;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InParsec(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Parsec);

        Vector3 actual = quantity.Parsecs;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InKiloparsec(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Kiloparsec);

        Vector3 actual = quantity.Kiloparsecs;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMegaparsec(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Megaparsec);

        Vector3 actual = quantity.Megaparsecs;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InGigaparsec(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Gigaparsec);

        Vector3 actual = quantity.Gigaparsecs;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InInch(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Inch);

        Vector3 actual = quantity.Inches;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InFoot(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Foot);

        Vector3 actual = quantity.Feet;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InYard(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Yard);

        Vector3 actual = quantity.Yards;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMile(Vector3 expected)
    {
        Displacement3 quantity = new(expected, UnitOfLength.Mile);

        Vector3 actual = quantity.Miles;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
