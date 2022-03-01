#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfVelocitySquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocitySquaredDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfVelocitySquared a, MetricPrefix prefix)
    {
        UnitOfVelocitySquared result = a.WithPrefix(prefix);

        Assert.Equal(a.SpeedSquared.Magnitude * prefix.Scale, result.SpeedSquared.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocitySquaredDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfVelocitySquared a, Scalar scalar)
    {
        UnitOfVelocitySquared result = a.ScaledBy(scalar);

        Assert.Equal(a.SpeedSquared.Magnitude * scalar.Magnitude, result.SpeedSquared.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocitySquaredDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfVelocitySquared a, double scalar)
    {
        UnitOfVelocitySquared result = a.ScaledBy(scalar);

        Assert.Equal(a.SpeedSquared.Magnitude * scalar, result.SpeedSquared.Magnitude, 2);
    }
}
