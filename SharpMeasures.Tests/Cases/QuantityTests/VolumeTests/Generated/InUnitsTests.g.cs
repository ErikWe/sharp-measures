#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.VolumeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVolumeDataset>))]
    public void InUnit(Scalar expected, UnitOfVolume unit)
    {
        Volume quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCubicMetre(Scalar expected)
    {
        Volume quantity = new(expected, UnitOfVolume.CubicMetre);

        Scalar actual = quantity.CubicMetres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCubicDecimetre(Scalar expected)
    {
        Volume quantity = new(expected, UnitOfVolume.CubicDecimetre);

        Scalar actual = quantity.CubicDecimetres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InLitre(Scalar expected)
    {
        Volume quantity = new(expected, UnitOfVolume.Litre);

        Scalar actual = quantity.Litres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMillilitre(Scalar expected)
    {
        Volume quantity = new(expected, UnitOfVolume.Millilitre);

        Scalar actual = quantity.Millilitres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCentilitre(Scalar expected)
    {
        Volume quantity = new(expected, UnitOfVolume.Centilitre);

        Scalar actual = quantity.Centilitres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InDecilitre(Scalar expected)
    {
        Volume quantity = new(expected, UnitOfVolume.Decilitre);

        Scalar actual = quantity.Decilitres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
