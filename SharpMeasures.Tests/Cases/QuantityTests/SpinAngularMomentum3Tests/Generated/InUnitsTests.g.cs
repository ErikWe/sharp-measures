#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngularMomentumDataset>))]
    public void InUnit(Vector3 expected, UnitOfAngularMomentum unit)
    {
        SpinAngularMomentum3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InKilogramSquareMetrePerSecond(Vector3 expected)
    {
        SpinAngularMomentum3 quantity = new(expected, UnitOfAngularMomentum.KilogramSquareMetrePerSecond);

        Vector3 actual = quantity.KilogramSquareMetresPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
