#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAreaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAreaDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfArea a, MetricPrefix prefix)
    {
        UnitOfArea result = a.WithPrefix(prefix);

        Assert.Equal(a.Area.Magnitude * prefix.Scale, result.Area.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAreaDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfArea a, Scalar scalar)
    {
        UnitOfArea result = a.ScaledBy(scalar);

        Assert.Equal(a.Area.Magnitude * scalar.Magnitude, result.Area.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAreaDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfArea a, double scalar)
    {
        UnitOfArea result = a.ScaledBy(scalar);

        Assert.Equal(a.Area.Magnitude * scalar, result.Area.Magnitude, 2);
    }
}
