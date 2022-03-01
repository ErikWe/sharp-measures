#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAngularVelocityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularVelocityDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfAngularVelocity a, MetricPrefix prefix)
    {
        UnitOfAngularVelocity result = a.WithPrefix(prefix);

        Assert.Equal(a.AngularSpeed.Magnitude * prefix.Scale, result.AngularSpeed.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularVelocityDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfAngularVelocity a, Scalar scalar)
    {
        UnitOfAngularVelocity result = a.ScaledBy(scalar);

        Assert.Equal(a.AngularSpeed.Magnitude * scalar.Magnitude, result.AngularSpeed.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularVelocityDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfAngularVelocity a, double scalar)
    {
        UnitOfAngularVelocity result = a.ScaledBy(scalar);

        Assert.Equal(a.AngularSpeed.Magnitude * scalar, result.AngularSpeed.Magnitude, 2);
    }
}
