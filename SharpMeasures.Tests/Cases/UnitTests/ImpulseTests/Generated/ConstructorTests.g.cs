#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfImpulseTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfImpulseDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfImpulse a, MetricPrefix prefix)
    {
        UnitOfImpulse result = a.WithPrefix(prefix);

        Assert.Equal(a.Impulse.Magnitude * prefix.Scale, result.Impulse.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfImpulseDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfImpulse a, Scalar scalar)
    {
        UnitOfImpulse result = a.ScaledBy(scalar);

        Assert.Equal(a.Impulse.Magnitude * scalar.Magnitude, result.Impulse.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfImpulseDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfImpulse a, double scalar)
    {
        UnitOfImpulse result = a.ScaledBy(scalar);

        Assert.Equal(a.Impulse.Magnitude * scalar, result.Impulse.Magnitude, 2);
    }
}
