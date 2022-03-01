#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfLengthTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLengthDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfLength a, MetricPrefix prefix)
    {
        UnitOfLength result = a.WithPrefix(prefix);

        Assert.Equal(a.Length.Magnitude * prefix.Scale, result.Length.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLengthDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfLength a, Scalar scalar)
    {
        UnitOfLength result = a.ScaledBy(scalar);

        Assert.Equal(a.Length.Magnitude * scalar.Magnitude, result.Length.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLengthDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfLength a, double scalar)
    {
        UnitOfLength result = a.ScaledBy(scalar);

        Assert.Equal(a.Length.Magnitude * scalar, result.Length.Magnitude, 2);
    }
}
