#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularAccelerationDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfAngularAcceleration a, MetricPrefix prefix)
    {
        UnitOfAngularAcceleration result = a.WithPrefix(prefix);

        Assert.Equal(a.AngularAcceleration.Magnitude * prefix.Scale, result.AngularAcceleration.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularAccelerationDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfAngularAcceleration a, Scalar scalar)
    {
        UnitOfAngularAcceleration result = a.ScaledBy(scalar);

        Assert.Equal(a.AngularAcceleration.Magnitude * scalar.Magnitude, result.AngularAcceleration.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularAccelerationDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfAngularAcceleration a, double scalar)
    {
        UnitOfAngularAcceleration result = a.ScaledBy(scalar);

        Assert.Equal(a.AngularAcceleration.Magnitude * scalar, result.AngularAcceleration.Magnitude, 2);
    }
}
