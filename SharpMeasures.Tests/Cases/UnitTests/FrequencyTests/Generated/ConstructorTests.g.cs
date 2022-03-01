#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfFrequencyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfFrequency a, MetricPrefix prefix)
    {
        UnitOfFrequency result = a.WithPrefix(prefix);

        Assert.Equal(a.Frequency.Magnitude * prefix.Scale, result.Frequency.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfFrequency a, Scalar scalar)
    {
        UnitOfFrequency result = a.ScaledBy(scalar);

        Assert.Equal(a.Frequency.Magnitude * scalar.Magnitude, result.Frequency.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfFrequency a, double scalar)
    {
        UnitOfFrequency result = a.ScaledBy(scalar);

        Assert.Equal(a.Frequency.Magnitude * scalar, result.Frequency.Magnitude, 2);
    }
}
