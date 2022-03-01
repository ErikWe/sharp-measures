#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpeedSquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVelocitySquaredDataset>))]
    public void InUnit(Scalar expected, UnitOfVelocitySquared unit)
    {
        SpeedSquared quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareMetrePerSecondSquared(Scalar expected)
    {
        SpeedSquared quantity = new(expected, UnitOfVelocitySquared.SquareMetrePerSecondSquared);

        Scalar actual = quantity.SquareMetresPerSecondSquared;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
