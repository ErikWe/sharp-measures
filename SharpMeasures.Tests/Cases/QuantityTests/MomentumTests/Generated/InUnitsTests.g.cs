#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMomentumDataset>))]
    public void InUnit(Scalar expected, UnitOfMomentum unit)
    {
        Momentum quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilogramMetrePerSecond(Scalar expected)
    {
        Momentum quantity = new(expected, UnitOfMomentum.KilogramMetrePerSecond);

        Scalar actual = quantity.KilogramMetresPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
