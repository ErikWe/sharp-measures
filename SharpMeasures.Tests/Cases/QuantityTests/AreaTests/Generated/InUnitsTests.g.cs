#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AreaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAreaDataset>))]
    public void InUnit(Scalar expected, UnitOfArea unit)
    {
        Area quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareMetre(Scalar expected)
    {
        Area quantity = new(expected, UnitOfArea.SquareMetre);

        Scalar actual = quantity.SquareMetres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareKilometre(Scalar expected)
    {
        Area quantity = new(expected, UnitOfArea.SquareKilometre);

        Scalar actual = quantity.SquareKilometres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareInch(Scalar expected)
    {
        Area quantity = new(expected, UnitOfArea.SquareInch);

        Scalar actual = quantity.SquareInches;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareMile(Scalar expected)
    {
        Area quantity = new(expected, UnitOfArea.SquareMile);

        Scalar actual = quantity.SquareMiles;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InAre(Scalar expected)
    {
        Area quantity = new(expected, UnitOfArea.Are);

        Scalar actual = quantity.Ares;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InHectare(Scalar expected)
    {
        Area quantity = new(expected, UnitOfArea.Hectare);

        Scalar actual = quantity.Hectares;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InAcre(Scalar expected)
    {
        Area quantity = new(expected, UnitOfArea.Acre);

        Scalar actual = quantity.Acres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
