#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.PressureTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfPressureDataset>))]
    public void InUnit(Scalar expected, UnitOfPressure unit)
    {
        Pressure quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPascal(Scalar expected)
    {
        Pressure quantity = new(expected, UnitOfPressure.Pascal);

        Scalar actual = quantity.Pascals;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InKilopascal(Scalar expected)
    {
        Pressure quantity = new(expected, UnitOfPressure.Kilopascal);

        Scalar actual = quantity.Kilopascals;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InBar(Scalar expected)
    {
        Pressure quantity = new(expected, UnitOfPressure.Bar);

        Scalar actual = quantity.Bars;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InStandardAtmosphere(Scalar expected)
    {
        Pressure quantity = new(expected, UnitOfPressure.StandardAtmosphere);

        Scalar actual = quantity.StandardAtmospheres;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InPoundForcePerSquareInch(Scalar expected)
    {
        Pressure quantity = new(expected, UnitOfPressure.PoundForcePerSquareInch);

        Scalar actual = quantity.PoundsForcePerSquareInch;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
