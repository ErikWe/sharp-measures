#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfLengthTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLengthDataset, UnitOfLengthDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfLength a, UnitOfLength b)
    {
        if (a.Length.CompareTo(b.Length) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Length.CompareTo(b.Length) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLengthDataset, UnitOfLengthDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfLength a, UnitOfLength b)
    {
        if (a.Length > b.Length)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Length < b.Length)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Length == b.Length)
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
