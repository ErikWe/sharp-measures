#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.JerkTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfJerkDataset>))]
    public void InUnit(Scalar expected, UnitOfJerk unit)
    {
        Jerk quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMetrePerSecondCubed(Scalar expected)
    {
        Jerk quantity = new(expected, UnitOfJerk.MetrePerSecondCubed);

        Scalar actual = quantity.MetresPerSecondCubed;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InFootPerSecondCubed(Scalar expected)
    {
        Jerk quantity = new(expected, UnitOfJerk.FootPerSecondCubed);

        Scalar actual = quantity.FootsPerSecondCubed;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
