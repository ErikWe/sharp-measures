#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfPressureTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPressureDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfPressure a, MetricPrefix prefix)
    {
        UnitOfPressure result = a.WithPrefix(prefix);

        Assert.Equal(a.Pressure.Magnitude * prefix.Scale, result.Pressure.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPressureDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfPressure a, Scalar scalar)
    {
        UnitOfPressure result = a.ScaledBy(scalar);

        Assert.Equal(a.Pressure.Magnitude * scalar.Magnitude, result.Pressure.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPressureDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfPressure a, double scalar)
    {
        UnitOfPressure result = a.ScaledBy(scalar);

        Assert.Equal(a.Pressure.Magnitude * scalar, result.Pressure.Magnitude, 2);
    }
}
