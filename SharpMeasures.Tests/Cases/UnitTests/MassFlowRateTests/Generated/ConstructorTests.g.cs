#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfMassFlowRateTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassFlowRateDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfMassFlowRate a, MetricPrefix prefix)
    {
        UnitOfMassFlowRate result = a.WithPrefix(prefix);

        Assert.Equal(a.MassFlowRate.Magnitude * prefix.Scale, result.MassFlowRate.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassFlowRateDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfMassFlowRate a, Scalar scalar)
    {
        UnitOfMassFlowRate result = a.ScaledBy(scalar);

        Assert.Equal(a.MassFlowRate.Magnitude * scalar.Magnitude, result.MassFlowRate.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassFlowRateDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfMassFlowRate a, double scalar)
    {
        UnitOfMassFlowRate result = a.ScaledBy(scalar);

        Assert.Equal(a.MassFlowRate.Magnitude * scalar, result.MassFlowRate.Magnitude, 2);
    }
}
