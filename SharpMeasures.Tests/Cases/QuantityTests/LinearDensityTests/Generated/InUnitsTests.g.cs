#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.LinearDensityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfLinearDensityDataset>))]
    public void InUnit(Scalar expected, UnitOfLinearDensity unit)
    {
        LinearDensity quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilogramPerMetre(Scalar expected)
    {
        LinearDensity quantity = new(expected, UnitOfLinearDensity.KilogramPerMetre);

        Scalar actual = quantity.KilogramsPerMetre;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
