#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularAccelerationDataset>))]
    public void InUnit(Scalar expected, UnitOfAngularAcceleration unit)
    {
        SpinAngularAcceleration quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InRadianPerSecondSquared(Scalar expected)
    {
        SpinAngularAcceleration quantity = new(expected, UnitOfAngularAcceleration.RadianPerSecondSquared);

        Scalar actual = quantity.RadiansPerSecondSquared;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
