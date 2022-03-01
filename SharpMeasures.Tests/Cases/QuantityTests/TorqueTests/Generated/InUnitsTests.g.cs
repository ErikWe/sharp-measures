#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TorqueTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfTorqueDataset>))]
    public void InUnit(Scalar expected, UnitOfTorque unit)
    {
        Torque quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InNewtonMetre(Scalar expected)
    {
        Torque quantity = new(expected, UnitOfTorque.NewtonMetre);

        Scalar actual = quantity.NewtonMetres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
