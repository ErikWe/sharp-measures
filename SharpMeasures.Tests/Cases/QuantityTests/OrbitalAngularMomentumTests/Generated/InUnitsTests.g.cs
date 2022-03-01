#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularMomentumDataset>))]
    public void InUnit(Scalar expected, UnitOfAngularMomentum unit)
    {
        OrbitalAngularMomentum quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilogramSquareMetrePerSecond(Scalar expected)
    {
        OrbitalAngularMomentum quantity = new(expected, UnitOfAngularMomentum.KilogramSquareMetrePerSecond);

        Scalar actual = quantity.KilogramSquareMetresPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
