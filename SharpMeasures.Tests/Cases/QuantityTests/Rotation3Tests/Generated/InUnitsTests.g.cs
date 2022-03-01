#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Rotation3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngleDataset>))]
    public void InUnit(Vector3 expected, UnitOfAngle unit)
    {
        Rotation3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InRadian(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Radian);

        Vector3 actual = quantity.Radians;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMilliradian(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Milliradian);

        Vector3 actual = quantity.Milliradians;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InDegree(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Degree);

        Vector3 actual = quantity.Degrees;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InGradian(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Gradian);

        Vector3 actual = quantity.Gradians;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InArcminute(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Arcminute);

        Vector3 actual = quantity.Arcminutes;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InArcsecond(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Arcsecond);

        Vector3 actual = quantity.Arcseconds;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMilliarcsecond(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Milliarcsecond);

        Vector3 actual = quantity.Milliarcseconds;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMicroarcsecond(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Microarcsecond);

        Vector3 actual = quantity.Microarcseconds;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InTurn(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Turn);

        Vector3 actual = quantity.Turns;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InHalfTurn(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.HalfTurn);

        Vector3 actual = quantity.HalfTurns;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InQuarterTurn(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.QuarterTurn);

        Vector3 actual = quantity.QuarterTurns;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InCentiturn(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Centiturn);

        Vector3 actual = quantity.Centiturns;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InMilliturn(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.Milliturn);

        Vector3 actual = quantity.Milliturns;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InBinaryDegree(Vector3 expected)
    {
        Rotation3 quantity = new(expected, UnitOfAngle.BinaryDegree);

        Vector3 actual = quantity.BinaryDegrees;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
