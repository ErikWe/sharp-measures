#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpatialFrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSpatialFrequencyDataset>))]
    public void InUnit(Scalar expected, UnitOfSpatialFrequency unit)
    {
        SpatialFrequency quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPerMetre(Scalar expected)
    {
        SpatialFrequency quantity = new(expected, UnitOfSpatialFrequency.PerMetre);

        Scalar actual = quantity.PerMetre;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
