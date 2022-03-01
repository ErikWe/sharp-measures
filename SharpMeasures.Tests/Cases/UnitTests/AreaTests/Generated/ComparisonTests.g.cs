#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAreaTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAreaDataset, UnitOfAreaDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfArea a, UnitOfArea b)
    {
        if (a.Area.CompareTo(b.Area) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Area.CompareTo(b.Area) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAreaDataset, UnitOfAreaDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfArea a, UnitOfArea b)
    {
        if (a.Area > b.Area)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Area < b.Area)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Area == b.Area)
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
