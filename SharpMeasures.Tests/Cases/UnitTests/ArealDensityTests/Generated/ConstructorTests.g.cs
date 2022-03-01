#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfArealDensityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfArealDensityDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfArealDensity a, MetricPrefix prefix)
    {
        UnitOfArealDensity result = a.WithPrefix(prefix);

        Assert.Equal(a.ArealDensity.Magnitude * prefix.Scale, result.ArealDensity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfArealDensityDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfArealDensity a, Scalar scalar)
    {
        UnitOfArealDensity result = a.ScaledBy(scalar);

        Assert.Equal(a.ArealDensity.Magnitude * scalar.Magnitude, result.ArealDensity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfArealDensityDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfArealDensity a, double scalar)
    {
        UnitOfArealDensity result = a.ScaledBy(scalar);

        Assert.Equal(a.ArealDensity.Magnitude * scalar, result.ArealDensity.Magnitude, 2);
    }
}
