#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngleTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngleDataset>))]
    public void InUnit(Scalar expected, UnitOfAngle unit)
    {
        Angle quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InRadian(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Radian);

        Scalar actual = quantity.Radians;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMilliradian(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Milliradian);

        Scalar actual = quantity.Milliradians;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InDegree(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Degree);

        Scalar actual = quantity.Degrees;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InGradian(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Gradian);

        Scalar actual = quantity.Gradians;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InArcminute(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Arcminute);

        Scalar actual = quantity.Arcminutes;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InArcsecond(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Arcsecond);

        Scalar actual = quantity.Arcseconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMilliarcsecond(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Milliarcsecond);

        Scalar actual = quantity.Milliarcseconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMicroarcsecond(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Microarcsecond);

        Scalar actual = quantity.Microarcseconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InTurn(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Turn);

        Scalar actual = quantity.Turns;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InHalfTurn(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.HalfTurn);

        Scalar actual = quantity.HalfTurns;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InQuarterTurn(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.QuarterTurn);

        Scalar actual = quantity.QuarterTurns;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InCentiturn(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Centiturn);

        Scalar actual = quantity.Centiturns;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InMilliturn(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.Milliturn);

        Scalar actual = quantity.Milliturns;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InBinaryDegree(Scalar expected)
    {
        Angle quantity = new(expected, UnitOfAngle.BinaryDegree);

        Scalar actual = quantity.BinaryDegrees;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
