#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.FrequencyDriftTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfFrequencyDriftDataset>))]
    public void InUnit(Scalar expected, UnitOfFrequencyDrift unit)
    {
        FrequencyDrift quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InHertzPerSecond(Scalar expected)
    {
        FrequencyDrift quantity = new(expected, UnitOfFrequencyDrift.HertzPerSecond);

        Scalar actual = quantity.HertzPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPerSecondSquared(Scalar expected)
    {
        FrequencyDrift quantity = new(expected, UnitOfFrequencyDrift.PerSecondSquared);

        Scalar actual = quantity.PerSecondSquared;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
