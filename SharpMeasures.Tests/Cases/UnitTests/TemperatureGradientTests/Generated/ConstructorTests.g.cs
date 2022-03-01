#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfTemperatureGradientTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureGradientDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfTemperatureGradient a, MetricPrefix prefix)
    {
        UnitOfTemperatureGradient result = a.WithPrefix(prefix);

        Assert.Equal(a.TemperatureGradient.Magnitude * prefix.Scale, result.TemperatureGradient.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureGradientDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfTemperatureGradient a, Scalar scalar)
    {
        UnitOfTemperatureGradient result = a.ScaledBy(scalar);

        Assert.Equal(a.TemperatureGradient.Magnitude * scalar.Magnitude, result.TemperatureGradient.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureGradientDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfTemperatureGradient a, double scalar)
    {
        UnitOfTemperatureGradient result = a.ScaledBy(scalar);

        Assert.Equal(a.TemperatureGradient.Magnitude * scalar, result.TemperatureGradient.Magnitude, 2);
    }
}
