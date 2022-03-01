#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfVolumeTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumeDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfVolume a, MetricPrefix prefix)
    {
        UnitOfVolume result = a.WithPrefix(prefix);

        Assert.Equal(a.Volume.Magnitude * prefix.Scale, result.Volume.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumeDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfVolume a, Scalar scalar)
    {
        UnitOfVolume result = a.ScaledBy(scalar);

        Assert.Equal(a.Volume.Magnitude * scalar.Magnitude, result.Volume.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumeDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfVolume a, double scalar)
    {
        UnitOfVolume result = a.ScaledBy(scalar);

        Assert.Equal(a.Volume.Magnitude * scalar, result.Volume.Magnitude, 2);
    }
}
