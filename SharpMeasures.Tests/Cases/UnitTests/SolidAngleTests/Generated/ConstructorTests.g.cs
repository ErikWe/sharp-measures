#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfSolidAngleTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSolidAngleDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfSolidAngle a, MetricPrefix prefix)
    {
        UnitOfSolidAngle result = a.WithPrefix(prefix);

        Assert.Equal(a.SolidAngle.Magnitude * prefix.Scale, result.SolidAngle.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSolidAngleDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfSolidAngle a, Scalar scalar)
    {
        UnitOfSolidAngle result = a.ScaledBy(scalar);

        Assert.Equal(a.SolidAngle.Magnitude * scalar.Magnitude, result.SolidAngle.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSolidAngleDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfSolidAngle a, double scalar)
    {
        UnitOfSolidAngle result = a.ScaledBy(scalar);

        Assert.Equal(a.SolidAngle.Magnitude * scalar, result.SolidAngle.Magnitude, 2);
    }
}
