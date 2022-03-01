#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfVelocityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocityDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfVelocity a, MetricPrefix prefix)
    {
        UnitOfVelocity result = a.WithPrefix(prefix);

        Assert.Equal(a.Speed.Magnitude * prefix.Scale, result.Speed.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocityDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfVelocity a, Scalar scalar)
    {
        UnitOfVelocity result = a.ScaledBy(scalar);

        Assert.Equal(a.Speed.Magnitude * scalar.Magnitude, result.Speed.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocityDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfVelocity a, double scalar)
    {
        UnitOfVelocity result = a.ScaledBy(scalar);

        Assert.Equal(a.Speed.Magnitude * scalar, result.Speed.Magnitude, 2);
    }
}
