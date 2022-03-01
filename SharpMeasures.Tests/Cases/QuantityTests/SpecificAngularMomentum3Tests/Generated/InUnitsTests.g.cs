#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificAngularMomentum3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfSpecificAngularMomentumDataset>))]
    public void InUnit(Vector3 expected, UnitOfSpecificAngularMomentum unit)
    {
        SpecificAngularMomentum3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InSquareMetrePerSecond(Vector3 expected)
    {
        SpecificAngularMomentum3 quantity = new(expected, UnitOfSpecificAngularMomentum.SquareMetrePerSecond);

        Vector3 actual = quantity.SquareMetresPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
