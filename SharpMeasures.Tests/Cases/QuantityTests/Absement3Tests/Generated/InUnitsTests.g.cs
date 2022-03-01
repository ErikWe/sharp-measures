#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Absement3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAbsementDataset>))]
    public void InUnit(Vector3 expected, UnitOfAbsement unit)
    {
        Absement3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMetreSecond(Vector3 expected)
    {
        Absement3 quantity = new(expected, UnitOfAbsement.MetreSecond);

        Vector3 actual = quantity.MetreSeconds;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
