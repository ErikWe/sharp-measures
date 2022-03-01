#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SolidAngleTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfSolidAngleDataset>))]
    public void InUnit(Scalar expected, UnitOfSolidAngle unit)
    {
        SolidAngle quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSteradian(Scalar expected)
    {
        SolidAngle quantity = new(expected, UnitOfSolidAngle.Steradian);

        Scalar actual = quantity.Steradians;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareRadian(Scalar expected)
    {
        SolidAngle quantity = new(expected, UnitOfSolidAngle.SquareRadian);

        Scalar actual = quantity.SquareRadians;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareDegree(Scalar expected)
    {
        SolidAngle quantity = new(expected, UnitOfSolidAngle.SquareDegree);

        Scalar actual = quantity.SquareDegrees;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareArcminute(Scalar expected)
    {
        SolidAngle quantity = new(expected, UnitOfSolidAngle.SquareArcminute);

        Scalar actual = quantity.SquareArcminutes;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InSquareArcsecond(Scalar expected)
    {
        SolidAngle quantity = new(expected, UnitOfSolidAngle.SquareArcsecond);

        Scalar actual = quantity.SquareArcseconds;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
