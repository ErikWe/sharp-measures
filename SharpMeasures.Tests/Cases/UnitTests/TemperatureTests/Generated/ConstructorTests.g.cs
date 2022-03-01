#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfTemperatureTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfTemperature a, MetricPrefix prefix)
    {
        UnitOfTemperature result = a.WithPrefix(prefix);

        Assert.Equal(a.TemperatureDifference.Magnitude * prefix.Scale, result.TemperatureDifference.Magnitude, 2);
        Assert.Equal(a.Offset / prefix.Scale, result.Offset);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfTemperature a, Scalar scalar)
    {
        UnitOfTemperature result = a.ScaledBy(scalar);

        Assert.Equal(a.TemperatureDifference.Magnitude * scalar.Magnitude, result.TemperatureDifference.Magnitude, 2);
        Assert.Equal(a.Offset / scalar, result.Offset.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureDataset, DoubleDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfTemperature a, double scalar)
    {
        UnitOfTemperature result = a.ScaledBy(scalar);

        Assert.Equal(a.TemperatureDifference.Magnitude * scalar, result.TemperatureDifference.Magnitude, 2);
        Assert.Equal(a.Offset / scalar, result.Offset, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureDataset, ScalarDataset>))]
    public void OffsetBy_Scalar_ShouldBeSum(UnitOfTemperature a, Scalar scalar)
    {
        UnitOfTemperature result = a.OffsetBy(scalar);

        Assert.Equal(a.TemperatureDifference.Magnitude, result.TemperatureDifference.Magnitude, 2);
        Assert.Equal(a.Offset + scalar, result.Offset, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureDataset, DoubleDataset>))]
    public void OffsetBy_Double_ShouldBeSum(UnitOfTemperature a, double scalar)
    {
        UnitOfTemperature result = a.OffsetBy(scalar);

        Assert.Equal(a.TemperatureDifference.Magnitude, result.TemperatureDifference.Magnitude, 2);
        Assert.Equal(a.Offset + scalar, result.Offset, 2);
    }
}
