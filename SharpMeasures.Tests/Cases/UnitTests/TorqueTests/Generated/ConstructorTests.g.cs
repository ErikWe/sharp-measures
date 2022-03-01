#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfTorqueTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTorqueDataset, MetricPrefixDataset>))]
    public void WithPrefix_ShouldBeProductWithPrefix(UnitOfTorque a, MetricPrefix prefix)
    {
        UnitOfTorque result = a.WithPrefix(prefix);

        Assert.Equal(a.Torque.Magnitude * prefix.Scale, result.Torque.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTorqueDataset, ScalarDataset>))]
    public void ScaledBy_Scalar_ShouldBeProduct(UnitOfTorque a, Scalar scalar)
    {
        UnitOfTorque result = a.ScaledBy(scalar);

        Assert.Equal(a.Torque.Magnitude * scalar.Magnitude, result.Torque.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTorqueDataset, ScalarDataset>))]
    public void ScaledBy_Double_ShouldBeProduct(UnitOfTorque a, double scalar)
    {
        UnitOfTorque result = a.ScaledBy(scalar);

        Assert.Equal(a.Torque.Magnitude * scalar, result.Torque.Magnitude, 2);
    }
}
