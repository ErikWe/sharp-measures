#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfDensityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfDensityDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfDensity a, MetricPrefix prefix)
    {
        UnitOfDensity result = a.WithPrefix(prefix);

        Assert.Equal(a.Density.Magnitude * prefix.Scale, result.Density.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfDensityDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfDensity a, Scalar scalar)
    {
        UnitOfDensity result = a.ScaledBy(scalar);

        Assert.Equal(a.Density.Magnitude * scalar.Magnitude, result.Density.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfDensityDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfDensity a, double scalar)
    {
        UnitOfDensity result = a.ScaledBy(scalar);

        Assert.Equal(a.Density.Magnitude * scalar, result.Density.Magnitude, 2);
    }
}
