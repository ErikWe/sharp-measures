#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Jerk3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfJerkDataset>))]
    public void InUnit(Vector3 expected, UnitOfJerk unit)
    {
        Jerk3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMetrePerSecondCubed(Vector3 expected)
    {
        Jerk3 quantity = new(expected, UnitOfJerk.MetrePerSecondCubed);

        Vector3 actual = quantity.MetresPerSecondCubed;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InFootPerSecondCubed(Vector3 expected)
    {
        Jerk3 quantity = new(expected, UnitOfJerk.FootPerSecondCubed);

        Vector3 actual = quantity.FootsPerSecondCubed;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
