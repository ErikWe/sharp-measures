#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.MetricPrefixTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using System;

using Xunit;

public class ConstructorTests
{
    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldBeEquivalent(double scale)
    {
        MetricPrefix result = new(scale);

        Assert.Equal(scale, result.Scale, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldBeEquivalent(Scalar scale)
    {
        MetricPrefix result = new(scale);

        Assert.Equal(scale, result.Scale, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void WithPowerOfTen_ShouldBeEquivalent(double power)
    {
        MetricPrefix result = MetricPrefix.WithPowerOfTen(power);

        Assert.Equal(Math.Pow(10, power), result.Scale, 2);
    }
}
