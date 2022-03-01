#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfSpecificAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificAngularMomentumDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfSpecificAngularMomentum a, MetricPrefix prefix)
    {
        UnitOfSpecificAngularMomentum result = a.WithPrefix(prefix);

        Assert.Equal(a.SpecificAngularMomentum.Magnitude * prefix.Scale, result.SpecificAngularMomentum.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificAngularMomentumDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfSpecificAngularMomentum a, Scalar scalar)
    {
        UnitOfSpecificAngularMomentum result = a.ScaledBy(scalar);

        Assert.Equal(a.SpecificAngularMomentum.Magnitude * scalar.Magnitude, result.SpecificAngularMomentum.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificAngularMomentumDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfSpecificAngularMomentum a, double scalar)
    {
        UnitOfSpecificAngularMomentum result = a.ScaledBy(scalar);

        Assert.Equal(a.SpecificAngularMomentum.Magnitude * scalar, result.SpecificAngularMomentum.Magnitude, 2);
    }
}
