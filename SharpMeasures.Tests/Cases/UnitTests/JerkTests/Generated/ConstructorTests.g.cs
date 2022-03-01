#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfJerkTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfJerkDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfJerk a, MetricPrefix prefix)
    {
        UnitOfJerk result = a.WithPrefix(prefix);

        Assert.Equal(a.Jerk.Magnitude * prefix.Scale, result.Jerk.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfJerkDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfJerk a, Scalar scalar)
    {
        UnitOfJerk result = a.ScaledBy(scalar);

        Assert.Equal(a.Jerk.Magnitude * scalar.Magnitude, result.Jerk.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfJerkDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfJerk a, double scalar)
    {
        UnitOfJerk result = a.ScaledBy(scalar);

        Assert.Equal(a.Jerk.Magnitude * scalar, result.Jerk.Magnitude, 2);
    }
}
