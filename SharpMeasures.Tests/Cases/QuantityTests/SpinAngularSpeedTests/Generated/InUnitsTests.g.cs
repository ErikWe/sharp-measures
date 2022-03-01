#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, UnitOfAngularVelocityDataset>))]
    public void InUnit(Scalar expected, UnitOfAngularVelocity unit)
    {
        SpinAngularSpeed quantity = new(expected, unit);

        Scalar actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InRadianPerSecond(Scalar expected)
    {
        SpinAngularSpeed quantity = new(expected, UnitOfAngularVelocity.RadianPerSecond);

        Scalar actual = quantity.RadiansPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InDegreePerSecond(Scalar expected)
    {
        SpinAngularSpeed quantity = new(expected, UnitOfAngularVelocity.DegreePerSecond);

        Scalar actual = quantity.DegreesPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InRevolutionPerSecond(Scalar expected)
    {
        SpinAngularSpeed quantity = new(expected, UnitOfAngularVelocity.RevolutionPerSecond);

        Scalar actual = quantity.RevolutionsPerSecond;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void InRevolutionPerMinute(Scalar expected)
    {
        SpinAngularSpeed quantity = new(expected, UnitOfAngularVelocity.RevolutionPerMinute);

        Scalar actual = quantity.RevolutionsPerMinute;

        Utility.AssertExtra.AssertEqualMagnitudes(expected, actual);
    }
}
