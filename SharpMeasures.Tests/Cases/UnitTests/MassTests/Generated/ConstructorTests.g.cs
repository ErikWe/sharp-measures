#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfMassTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfMass a, MetricPrefix prefix)
    {
        UnitOfMass result = a.WithPrefix(prefix);

        Assert.Equal(a.Mass.Magnitude * prefix.Scale, result.Mass.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfMass a, Scalar scalar)
    {
        UnitOfMass result = a.ScaledBy(scalar);

        Assert.Equal(a.Mass.Magnitude * scalar.Magnitude, result.Mass.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfMass a, double scalar)
    {
        UnitOfMass result = a.ScaledBy(scalar);

        Assert.Equal(a.Mass.Magnitude * scalar, result.Mass.Magnitude, 2);
    }
}
