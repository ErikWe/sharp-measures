#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfSpatialFrequencyTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpatialFrequencyDataset, UnitOfSpatialFrequencyDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfSpatialFrequency a, UnitOfSpatialFrequency b)
    {
        if (a.SpatialFrequency.CompareTo(b.SpatialFrequency) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.SpatialFrequency.CompareTo(b.SpatialFrequency) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpatialFrequencyDataset, UnitOfSpatialFrequencyDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfSpatialFrequency a, UnitOfSpatialFrequency b)
    {
        if (a.SpatialFrequency > b.SpatialFrequency)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.SpatialFrequency < b.SpatialFrequency)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.SpatialFrequency == b.SpatialFrequency)
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
