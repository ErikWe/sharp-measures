#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MassTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMassDataset>))]
    public void InUnit(Scalar expected, UnitOfMass unit)
    {
        Mass quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InGram(Scalar expected)
    {
        Mass quantity = new(expected, UnitOfMass.Gram);

        Scalar actual = quantity.Grams;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMilligram(Scalar expected)
    {
        Mass quantity = new(expected, UnitOfMass.Milligram);

        Scalar actual = quantity.Milligrams;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InHectogram(Scalar expected)
    {
        Mass quantity = new(expected, UnitOfMass.Hectogram);

        Scalar actual = quantity.Hectograms;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilogram(Scalar expected)
    {
        Mass quantity = new(expected, UnitOfMass.Kilogram);

        Scalar actual = quantity.Kilograms;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InTonne(Scalar expected)
    {
        Mass quantity = new(expected, UnitOfMass.Tonne);

        Scalar actual = quantity.Tonnes;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InOunce(Scalar expected)
    {
        Mass quantity = new(expected, UnitOfMass.Ounce);

        Scalar actual = quantity.Ounces;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPound(Scalar expected)
    {
        Mass quantity = new(expected, UnitOfMass.Pound);

        Scalar actual = quantity.Pounds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
