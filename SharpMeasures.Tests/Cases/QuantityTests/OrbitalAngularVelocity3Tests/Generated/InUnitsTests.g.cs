#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularVelocity3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class InUnitsTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnitOfAngularVelocityDataset>))]
    public void InUnit(Vector3 expected, UnitOfAngularVelocity unit)
    {
        OrbitalAngularVelocity3 quantity = new(expected, unit);

        Vector3 actual = quantity.InUnit(unit);

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InRadianPerSecond(Vector3 expected)
    {
        OrbitalAngularVelocity3 quantity = new(expected, UnitOfAngularVelocity.RadianPerSecond);

        Vector3 actual = quantity.RadiansPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InDegreePerSecond(Vector3 expected)
    {
        OrbitalAngularVelocity3 quantity = new(expected, UnitOfAngularVelocity.DegreePerSecond);

        Vector3 actual = quantity.DegreesPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InRevolutionPerSecond(Vector3 expected)
    {
        OrbitalAngularVelocity3 quantity = new(expected, UnitOfAngularVelocity.RevolutionPerSecond);

        Vector3 actual = quantity.RevolutionsPerSecond;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void InRevolutionPerMinute(Vector3 expected)
    {
        OrbitalAngularVelocity3 quantity = new(expected, UnitOfAngularVelocity.RevolutionPerMinute);

        Vector3 actual = quantity.RevolutionsPerMinute;

        Utility.AssertExtra.AssertEqualComponents(expected, actual);
    }
}
