#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Force3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfForceDataset>))]
    public void InUnit(Vector3 expected, UnitOfForce unit)
    {
        Force3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InNewton(Vector3 expected)
    {
        Force3 quantity = new(expected, UnitOfForce.Newton);

        Vector3 actual = quantity.Newtons;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InPoundForce(Vector3 expected)
    {
        Force3 quantity = new(expected, UnitOfForce.PoundForce);

        Vector3 actual = quantity.PoundsForce;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
