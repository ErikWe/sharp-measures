#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ArealDensityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfArealDensityDataset>))]
    public void InUnit(Scalar expected, UnitOfArealDensity unit)
    {
        ArealDensity quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilogramPerSquareMetre(Scalar expected)
    {
        ArealDensity quantity = new(expected, UnitOfArealDensity.KilogramPerSquareMetre);

        Scalar actual = quantity.KilogramsPerSquareMetre;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
