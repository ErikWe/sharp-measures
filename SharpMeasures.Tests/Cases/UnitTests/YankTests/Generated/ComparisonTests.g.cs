#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfYankTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfYankDataset, UnitOfYankDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfYank a, UnitOfYank b)
    {
        if (a.Yank.CompareTo(b.Yank) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Yank.CompareTo(b.Yank) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfYankDataset, UnitOfYankDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfYank a, UnitOfYank b)
    {
        if (a.Yank > b.Yank)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Yank < b.Yank)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Yank == b.Yank)
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
