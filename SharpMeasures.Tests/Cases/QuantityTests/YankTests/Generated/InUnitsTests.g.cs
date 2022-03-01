#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.YankTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfYankDataset>))]
    public void InUnit(Scalar expected, UnitOfYank unit)
    {
        Yank quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InNewtonPerSecond(Scalar expected)
    {
        Yank quantity = new(expected, UnitOfYank.NewtonPerSecond);

        Scalar actual = quantity.NewtonsPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
