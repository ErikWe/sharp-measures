#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfMomentOfInertiaTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentOfInertiaDataset, UnitOfMomentOfInertiaDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfMomentOfInertia a, UnitOfMomentOfInertia b)
    {
        if (a.MomentOfInertia.CompareTo(b.MomentOfInertia) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.MomentOfInertia.CompareTo(b.MomentOfInertia) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMomentOfInertiaDataset, UnitOfMomentOfInertiaDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfMomentOfInertia a, UnitOfMomentOfInertia b)
    {
        if (a.MomentOfInertia > b.MomentOfInertia)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.MomentOfInertia < b.MomentOfInertia)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.MomentOfInertia == b.MomentOfInertia)
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
