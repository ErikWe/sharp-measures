#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfPowerTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPowerDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfPower a, MetricPrefix prefix)
    {
        UnitOfPower result = a.WithPrefix(prefix);

        Assert.Equal(a.Power.Magnitude * prefix.Scale, result.Power.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPowerDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfPower a, Scalar scalar)
    {
        UnitOfPower result = a.ScaledBy(scalar);

        Assert.Equal(a.Power.Magnitude * scalar.Magnitude, result.Power.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPowerDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfPower a, double scalar)
    {
        UnitOfPower result = a.ScaledBy(scalar);

        Assert.Equal(a.Power.Magnitude * scalar, result.Power.Magnitude, 2);
    }
}
