#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.MetricPrefixTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<MetricPrefixDataset, MetricPrefixDataset>))]
    public void Method_ShouldMatchDouble(MetricPrefix a, MetricPrefix b)
    {
        if (a.Scale.CompareTo(b.Scale) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Scale.CompareTo(b.Scale) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<MetricPrefixDataset, MetricPrefixDataset>))]
    public void Operator_ShouldMatchDouble(MetricPrefix a, MetricPrefix b)
    {
        if (a.Scale > b.Scale)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Scale < b.Scale)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Scale == b.Scale)
        {
            Assert.False(a > b);
            Assert.True(a >= b);
            Assert.True(a <= b);
            Assert.False(a < b);
        }
        else
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
    }
}
