#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfForceTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfForceDataset, UnitOfForceDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfForce a, UnitOfForce b)
    {
        if (a.Force.CompareTo(b.Force) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Force.CompareTo(b.Force) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfForceDataset, UnitOfForceDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfForce a, UnitOfForce b)
    {
        if (a.Force > b.Force)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Force < b.Force)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Force == b.Force)
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
