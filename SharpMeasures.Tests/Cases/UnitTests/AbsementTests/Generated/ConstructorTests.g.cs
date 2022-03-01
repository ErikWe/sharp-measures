#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAbsementTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAbsementDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfAbsement a, MetricPrefix prefix)
    {
        UnitOfAbsement result = a.WithPrefix(prefix);

        Assert.Equal(a.Absement.Magnitude * prefix.Scale, result.Absement.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAbsementDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfAbsement a, Scalar scalar)
    {
        UnitOfAbsement result = a.ScaledBy(scalar);

        Assert.Equal(a.Absement.Magnitude * scalar.Magnitude, result.Absement.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAbsementDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfAbsement a, double scalar)
    {
        UnitOfAbsement result = a.ScaledBy(scalar);

        Assert.Equal(a.Absement.Magnitude * scalar, result.Absement.Magnitude, 2);
    }
}
