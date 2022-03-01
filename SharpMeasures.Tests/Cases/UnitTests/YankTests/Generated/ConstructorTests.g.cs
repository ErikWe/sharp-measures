#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfYankTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfYankDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfYank a, MetricPrefix prefix)
    {
        UnitOfYank result = a.WithPrefix(prefix);

        Assert.Equal(a.Yank.Magnitude * prefix.Scale, result.Yank.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfYankDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfYank a, Scalar scalar)
    {
        UnitOfYank result = a.ScaledBy(scalar);

        Assert.Equal(a.Yank.Magnitude * scalar.Magnitude, result.Yank.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfYankDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfYank a, double scalar)
    {
        UnitOfYank result = a.ScaledBy(scalar);

        Assert.Equal(a.Yank.Magnitude * scalar, result.Yank.Magnitude, 2);
    }
}
