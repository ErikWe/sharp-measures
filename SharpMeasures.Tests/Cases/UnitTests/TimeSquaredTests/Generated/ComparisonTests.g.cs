#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfTimeSquaredTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeSquaredDataset, UnitOfTimeSquaredDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfTimeSquared a, UnitOfTimeSquared b)
    {
        if (a.TimeSquared.CompareTo(b.TimeSquared) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.TimeSquared.CompareTo(b.TimeSquared) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTimeSquaredDataset, UnitOfTimeSquaredDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfTimeSquared a, UnitOfTimeSquared b)
    {
        if (a.TimeSquared > b.TimeSquared)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.TimeSquared < b.TimeSquared)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.TimeSquared == b.TimeSquared)
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
