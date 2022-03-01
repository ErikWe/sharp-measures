#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfMomentOfInertiaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentOfInertiaDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfMomentOfInertia a, MetricPrefix prefix)
    {
        UnitOfMomentOfInertia result = a.WithPrefix(prefix);

        Assert.Equal(a.MomentOfInertia.Magnitude * prefix.Scale, result.MomentOfInertia.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentOfInertiaDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfMomentOfInertia a, Scalar scalar)
    {
        UnitOfMomentOfInertia result = a.ScaledBy(scalar);

        Assert.Equal(a.MomentOfInertia.Magnitude * scalar.Magnitude, result.MomentOfInertia.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentOfInertiaDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfMomentOfInertia a, double scalar)
    {
        UnitOfMomentOfInertia result = a.ScaledBy(scalar);

        Assert.Equal(a.MomentOfInertia.Magnitude * scalar, result.MomentOfInertia.Magnitude, 2);
    }
}
