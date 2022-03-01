#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfVolumetricFlowRateTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumetricFlowRateDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfVolumetricFlowRate a, MetricPrefix prefix)
    {
        UnitOfVolumetricFlowRate result = a.WithPrefix(prefix);

        Assert.Equal(a.VolumetricFlowRate.Magnitude * prefix.Scale, result.VolumetricFlowRate.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumetricFlowRateDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfVolumetricFlowRate a, Scalar scalar)
    {
        UnitOfVolumetricFlowRate result = a.ScaledBy(scalar);

        Assert.Equal(a.VolumetricFlowRate.Magnitude * scalar.Magnitude, result.VolumetricFlowRate.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumetricFlowRateDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfVolumetricFlowRate a, double scalar)
    {
        UnitOfVolumetricFlowRate result = a.ScaledBy(scalar);

        Assert.Equal(a.VolumetricFlowRate.Magnitude * scalar, result.VolumetricFlowRate.Magnitude, 2);
    }
}
