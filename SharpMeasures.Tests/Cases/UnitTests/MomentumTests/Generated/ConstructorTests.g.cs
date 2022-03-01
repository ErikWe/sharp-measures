#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentumDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfMomentum a, MetricPrefix prefix)
    {
        UnitOfMomentum result = a.WithPrefix(prefix);

        Assert.Equal(a.Momentum.Magnitude * prefix.Scale, result.Momentum.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentumDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfMomentum a, Scalar scalar)
    {
        UnitOfMomentum result = a.ScaledBy(scalar);

        Assert.Equal(a.Momentum.Magnitude * scalar.Magnitude, result.Momentum.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentumDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfMomentum a, double scalar)
    {
        UnitOfMomentum result = a.ScaledBy(scalar);

        Assert.Equal(a.Momentum.Magnitude * scalar, result.Momentum.Magnitude, 2);
    }
}
