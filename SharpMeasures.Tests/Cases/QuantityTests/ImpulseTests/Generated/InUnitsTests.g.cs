#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ImpulseTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfImpulseDataset>))]
    public void InUnit(Scalar expected, UnitOfImpulse unit)
    {
        Impulse quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InNewtonSecond(Scalar expected)
    {
        Impulse quantity = new(expected, UnitOfImpulse.NewtonSecond);

        Scalar actual = quantity.NewtonSeconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
