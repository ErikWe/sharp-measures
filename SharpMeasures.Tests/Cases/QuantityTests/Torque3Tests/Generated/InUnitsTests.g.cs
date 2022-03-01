#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Torque3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfTorqueDataset>))]
    public void InUnit(Vector3 expected, UnitOfTorque unit)
    {
        Torque3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InNewtonMetre(Vector3 expected)
    {
        Torque3 quantity = new(expected, UnitOfTorque.NewtonMetre);

        Vector3 actual = quantity.NewtonMetres;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
