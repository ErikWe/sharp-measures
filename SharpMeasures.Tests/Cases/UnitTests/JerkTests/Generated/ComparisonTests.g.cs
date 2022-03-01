#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfJerkTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfJerkDataset, UnitOfJerkDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfJerk a, UnitOfJerk b)
    {
        if (a.Jerk.CompareTo(b.Jerk) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Jerk.CompareTo(b.Jerk) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfJerkDataset, UnitOfJerkDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfJerk a, UnitOfJerk b)
    {
        if (a.Jerk > b.Jerk)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Jerk < b.Jerk)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Jerk == b.Jerk)
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
