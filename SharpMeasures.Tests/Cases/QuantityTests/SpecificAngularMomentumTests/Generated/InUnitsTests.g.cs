#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpecificAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSpecificAngularMomentumDataset>))]
    public void InUnit(Scalar expected, UnitOfSpecificAngularMomentum unit)
    {
        SpecificAngularMomentum quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareMetrePerSecond(Scalar expected)
    {
        SpecificAngularMomentum quantity = new(expected, UnitOfSpecificAngularMomentum.SquareMetrePerSecond);

        Scalar actual = quantity.SquareMetresPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
