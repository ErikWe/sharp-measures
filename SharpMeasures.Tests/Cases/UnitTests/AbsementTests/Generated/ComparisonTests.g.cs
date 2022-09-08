#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAbsementTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAbsementDataset, UnitOfAbsementDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfAbsement a, UnitOfAbsement b)
    {
        if (a.Absement.CompareTo(b.Absement) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Absement.CompareTo(b.Absement) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAbsementDataset, UnitOfAbsementDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfAbsement a, UnitOfAbsement b)
    {
        if (a.Absement > b.Absement)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Absement < b.Absement)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Absement == b.Absement)
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