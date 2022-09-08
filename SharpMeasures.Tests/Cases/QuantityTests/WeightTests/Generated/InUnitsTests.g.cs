#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.WeightTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfForceDataset>))]
    public void InUnit(Scalar expected, UnitOfForce unit)
    {
        Weight quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InNewton(Scalar expected)
    {
        Weight quantity = new(expected, UnitOfForce.Newton);

        Scalar actual = quantity.Newtons;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPoundForce(Scalar expected)
    {
        Weight quantity = new(expected, UnitOfForce.PoundForce);

        Scalar actual = quantity.PoundsForce;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}