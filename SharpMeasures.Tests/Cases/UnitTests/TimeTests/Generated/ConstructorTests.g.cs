#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfTimeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfTime a, MetricPrefix prefix)
    {
        UnitOfTime result = a.WithPrefix(prefix);

        Assert.Equal(a.Time.Magnitude * prefix.Scale, result.Time.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfTime a, Scalar scalar)
    {
        UnitOfTime result = a.ScaledBy(scalar);

        Assert.Equal(a.Time.Magnitude * scalar.Magnitude, result.Time.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfTime a, double scalar)
    {
        UnitOfTime result = a.ScaledBy(scalar);

        Assert.Equal(a.Time.Magnitude * scalar, result.Time.Magnitude, 2);
    }
}
