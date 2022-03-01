#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAccelerationDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfAcceleration a, MetricPrefix prefix)
    {
        UnitOfAcceleration result = a.WithPrefix(prefix);

        Assert.Equal(a.Acceleration.Magnitude * prefix.Scale, result.Acceleration.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAccelerationDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfAcceleration a, Scalar scalar)
    {
        UnitOfAcceleration result = a.ScaledBy(scalar);

        Assert.Equal(a.Acceleration.Magnitude * scalar.Magnitude, result.Acceleration.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAccelerationDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfAcceleration a, double scalar)
    {
        UnitOfAcceleration result = a.ScaledBy(scalar);

        Assert.Equal(a.Acceleration.Magnitude * scalar, result.Acceleration.Magnitude, 2);
    }
}
