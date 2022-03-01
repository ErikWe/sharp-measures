#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfLinearDensityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLinearDensityDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfLinearDensity a, MetricPrefix prefix)
    {
        UnitOfLinearDensity result = a.WithPrefix(prefix);

        Assert.Equal(a.LinearDensity.Magnitude * prefix.Scale, result.LinearDensity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLinearDensityDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfLinearDensity a, Scalar scalar)
    {
        UnitOfLinearDensity result = a.ScaledBy(scalar);

        Assert.Equal(a.LinearDensity.Magnitude * scalar.Magnitude, result.LinearDensity.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLinearDensityDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfLinearDensity a, double scalar)
    {
        UnitOfLinearDensity result = a.ScaledBy(scalar);

        Assert.Equal(a.LinearDensity.Magnitude * scalar, result.LinearDensity.Magnitude, 2);
    }
}
