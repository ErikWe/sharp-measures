#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAngleTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngleDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfAngle a, MetricPrefix prefix)
    {
        UnitOfAngle result = a.WithPrefix(prefix);

        Assert.Equal(a.Angle.Magnitude * prefix.Scale, result.Angle.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngleDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfAngle a, Scalar scalar)
    {
        UnitOfAngle result = a.ScaledBy(scalar);

        Assert.Equal(a.Angle.Magnitude * scalar.Magnitude, result.Angle.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngleDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfAngle a, double scalar)
    {
        UnitOfAngle result = a.ScaledBy(scalar);

        Assert.Equal(a.Angle.Magnitude * scalar, result.Angle.Magnitude, 2);
    }
}
