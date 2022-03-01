#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MomentOfInertiaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfMomentOfInertiaDataset>))]
    public void InUnit(Scalar expected, UnitOfMomentOfInertia unit)
    {
        MomentOfInertia quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilogramSquareMetre(Scalar expected)
    {
        MomentOfInertia quantity = new(expected, UnitOfMomentOfInertia.KilogramSquareMetre);

        Scalar actual = quantity.KilogramSquareMetres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
