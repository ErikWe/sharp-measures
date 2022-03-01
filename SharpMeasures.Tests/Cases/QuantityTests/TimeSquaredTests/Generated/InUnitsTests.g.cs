#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TimeSquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTimeSquaredDataset>))]
    public void InUnit(Scalar expected, UnitOfTimeSquared unit)
    {
        TimeSquared quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareSecond(Scalar expected)
    {
        TimeSquared quantity = new(expected, UnitOfTimeSquared.SquareSecond);

        Scalar actual = quantity.SquareSeconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
