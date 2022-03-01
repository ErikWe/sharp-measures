#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.VolumetricFlowRateTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfVolumetricFlowRateDataset>))]
    public void InUnit(Scalar expected, UnitOfVolumetricFlowRate unit)
    {
        VolumetricFlowRate quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCubicMetrePerSecond(Scalar expected)
    {
        VolumetricFlowRate quantity = new(expected, UnitOfVolumetricFlowRate.CubicMetrePerSecond);

        Scalar actual = quantity.CubicMetresPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InLitrePerSecond(Scalar expected)
    {
        VolumetricFlowRate quantity = new(expected, UnitOfVolumetricFlowRate.LitrePerSecond);

        Scalar actual = quantity.LitresPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
