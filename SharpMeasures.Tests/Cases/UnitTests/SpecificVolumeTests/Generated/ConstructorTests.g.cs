#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfSpecificVolumeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificVolumeDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfSpecificVolume a, MetricPrefix prefix)
    {
        UnitOfSpecificVolume result = a.WithPrefix(prefix);

        Assert.Equal(a.SpecificVolume.Magnitude * prefix.Scale, result.SpecificVolume.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificVolumeDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfSpecificVolume a, Scalar scalar)
    {
        UnitOfSpecificVolume result = a.ScaledBy(scalar);

        Assert.Equal(a.SpecificVolume.Magnitude * scalar.Magnitude, result.SpecificVolume.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificVolumeDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfSpecificVolume a, double scalar)
    {
        UnitOfSpecificVolume result = a.ScaledBy(scalar);

        Assert.Equal(a.SpecificVolume.Magnitude * scalar, result.SpecificVolume.Magnitude, 2);
    }
}
