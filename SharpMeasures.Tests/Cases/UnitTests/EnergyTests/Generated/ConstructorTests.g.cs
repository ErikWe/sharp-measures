#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfEnergyDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfEnergy a, MetricPrefix prefix)
    {
        UnitOfEnergy result = a.WithPrefix(prefix);

        Assert.Equal(a.Energy.Magnitude * prefix.Scale, result.Energy.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfEnergyDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfEnergy a, Scalar scalar)
    {
        UnitOfEnergy result = a.ScaledBy(scalar);

        Assert.Equal(a.Energy.Magnitude * scalar.Magnitude, result.Energy.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfEnergyDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfEnergy a, double scalar)
    {
        UnitOfEnergy result = a.ScaledBy(scalar);

        Assert.Equal(a.Energy.Magnitude * scalar, result.Energy.Magnitude, 2);
    }
}
