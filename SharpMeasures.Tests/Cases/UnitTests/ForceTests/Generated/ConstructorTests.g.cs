#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfForceTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfForceDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfForce a, MetricPrefix prefix)
    {
        UnitOfForce result = a.WithPrefix(prefix);

        Assert.Equal(a.Force.Magnitude * prefix.Scale, result.Force.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfForceDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfForce a, Scalar scalar)
    {
        UnitOfForce result = a.ScaledBy(scalar);

        Assert.Equal(a.Force.Magnitude * scalar.Magnitude, result.Force.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfForceDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfForce a, double scalar)
    {
        UnitOfForce result = a.ScaledBy(scalar);

        Assert.Equal(a.Force.Magnitude * scalar, result.Force.Magnitude, 2);
    }
}
