#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfTimeSquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeSquaredDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfTimeSquared a, MetricPrefix prefix)
    {
        UnitOfTimeSquared result = a.WithPrefix(prefix);

        Assert.Equal(a.TimeSquared.Magnitude * prefix.Scale, result.TimeSquared.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeSquaredDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfTimeSquared a, Scalar scalar)
    {
        UnitOfTimeSquared result = a.ScaledBy(scalar);

        Assert.Equal(a.TimeSquared.Magnitude * scalar.Magnitude, result.TimeSquared.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeSquaredDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfTimeSquared a, double scalar)
    {
        UnitOfTimeSquared result = a.ScaledBy(scalar);

        Assert.Equal(a.TimeSquared.Magnitude * scalar, result.TimeSquared.Magnitude, 2);
    }
}
