#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularMomentumDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfAngularMomentum a, MetricPrefix prefix)
    {
        UnitOfAngularMomentum result = a.WithPrefix(prefix);

        Assert.Equal(a.AngularMomentum.Magnitude * prefix.Scale, result.AngularMomentum.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularMomentumDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfAngularMomentum a, Scalar scalar)
    {
        UnitOfAngularMomentum result = a.ScaledBy(scalar);

        Assert.Equal(a.AngularMomentum.Magnitude * scalar.Magnitude, result.AngularMomentum.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularMomentumDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfAngularMomentum a, double scalar)
    {
        UnitOfAngularMomentum result = a.ScaledBy(scalar);

        Assert.Equal(a.AngularMomentum.Magnitude * scalar, result.AngularMomentum.Magnitude, 2);
    }
}
