#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Impulse3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfImpulseDataset>))]
    public void InUnit(Vector3 expected, UnitOfImpulse unit)
    {
        Impulse3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InNewtonSecond(Vector3 expected)
    {
        Impulse3 quantity = new(expected, UnitOfImpulse.NewtonSecond);

        Vector3 actual = quantity.NewtonSeconds;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
