#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfTimeTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeDataset, UnitOfTimeDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfTime a, UnitOfTime b)
    {
        if (a.Time.CompareTo(b.Time) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Time.CompareTo(b.Time) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeDataset, UnitOfTimeDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfTime a, UnitOfTime b)
    {
        if (a.Time > b.Time)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Time < b.Time)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Time == b.Time)
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
