#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MassFlowRateTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMassFlowRateDataset>))]
    public void InUnit(Scalar expected, UnitOfMassFlowRate unit)
    {
        MassFlowRate quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilogramPerSecond(Scalar expected)
    {
        MassFlowRate quantity = new(expected, UnitOfMassFlowRate.KilogramPerSecond);

        Scalar actual = quantity.KilogramsPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPoundPerSecond(Scalar expected)
    {
        MassFlowRate quantity = new(expected, UnitOfMassFlowRate.PoundPerSecond);

        Scalar actual = quantity.PoundsPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
