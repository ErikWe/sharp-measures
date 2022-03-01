#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AbsementTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAbsementDataset>))]
    public void InUnit(Scalar expected, UnitOfAbsement unit)
    {
        Absement quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMetreSecond(Scalar expected)
    {
        Absement quantity = new(expected, UnitOfAbsement.MetreSecond);

        Scalar actual = quantity.MetreSeconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
