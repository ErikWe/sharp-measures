#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngularAccelerationDataset>))]
    public void InUnit(Vector3 expected, UnitOfAngularAcceleration unit)
    {
        AngularAcceleration3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InRadianPerSecondSquared(Vector3 expected)
    {
        AngularAcceleration3 quantity = new(expected, UnitOfAngularAcceleration.RadianPerSecondSquared);

        Vector3 actual = quantity.RadiansPerSecondSquared;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
