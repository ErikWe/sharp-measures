#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.DensityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfDensityDataset>))]
    public void InUnit(Scalar expected, UnitOfDensity unit)
    {
        Density quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilogramPerCubicMetre(Scalar expected)
    {
        Density quantity = new(expected, UnitOfDensity.KilogramPerCubicMetre);

        Scalar actual = quantity.KilogramsPerCubicMetre;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
