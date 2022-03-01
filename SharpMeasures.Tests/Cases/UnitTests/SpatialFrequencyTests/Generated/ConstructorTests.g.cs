#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfSpatialFrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpatialFrequencyDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfSpatialFrequency a, MetricPrefix prefix)
    {
        UnitOfSpatialFrequency result = a.WithPrefix(prefix);

        Assert.Equal(a.SpatialFrequency.Magnitude * prefix.Scale, result.SpatialFrequency.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpatialFrequencyDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfSpatialFrequency a, Scalar scalar)
    {
        UnitOfSpatialFrequency result = a.ScaledBy(scalar);

        Assert.Equal(a.SpatialFrequency.Magnitude * scalar.Magnitude, result.SpatialFrequency.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpatialFrequencyDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfSpatialFrequency a, double scalar)
    {
        UnitOfSpatialFrequency result = a.ScaledBy(scalar);

        Assert.Equal(a.SpatialFrequency.Magnitude * scalar, result.SpatialFrequency.Magnitude, 2);
    }
}
