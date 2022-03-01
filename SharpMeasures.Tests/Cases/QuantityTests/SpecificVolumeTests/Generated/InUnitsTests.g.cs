#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificVolumeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSpecificVolumeDataset>))]
    public void InUnit(Scalar expected, UnitOfSpecificVolume unit)
    {
        SpecificVolume quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCubicMetrePerKilogram(Scalar expected)
    {
        SpecificVolume quantity = new(expected, UnitOfSpecificVolume.CubicMetrePerKilogram);

        Scalar actual = quantity.CubicMetresPerKilogram;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
