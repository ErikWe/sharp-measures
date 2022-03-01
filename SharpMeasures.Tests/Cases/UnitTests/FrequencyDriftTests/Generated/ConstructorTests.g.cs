#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfFrequencyDriftTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDriftDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfFrequencyDrift a, MetricPrefix prefix)
    {
        UnitOfFrequencyDrift result = a.WithPrefix(prefix);

        Assert.Equal(a.FrequencyDrift.Magnitude * prefix.Scale, result.FrequencyDrift.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDriftDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfFrequencyDrift a, Scalar scalar)
    {
        UnitOfFrequencyDrift result = a.ScaledBy(scalar);

        Assert.Equal(a.FrequencyDrift.Magnitude * scalar.Magnitude, result.FrequencyDrift.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDriftDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfFrequencyDrift a, double scalar)
    {
        UnitOfFrequencyDrift result = a.ScaledBy(scalar);

        Assert.Equal(a.FrequencyDrift.Magnitude * scalar, result.FrequencyDrift.Magnitude, 2);
    }
}
