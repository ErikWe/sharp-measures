#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfMomentumTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentumDataset, UnitOfMomentumDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfMomentum a, UnitOfMomentum b)
    {
        if (a.Momentum.CompareTo(b.Momentum) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Momentum.CompareTo(b.Momentum) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentumDataset, UnitOfMomentumDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfMomentum a, UnitOfMomentum b)
    {
        if (a.Momentum > b.Momentum)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Momentum < b.Momentum)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Momentum == b.Momentum)
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
