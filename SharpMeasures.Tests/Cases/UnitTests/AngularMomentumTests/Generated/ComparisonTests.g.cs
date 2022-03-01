#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAngularMomentumTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularMomentumDataset, UnitOfAngularMomentumDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfAngularMomentum a, UnitOfAngularMomentum b)
    {
        if (a.AngularMomentum.CompareTo(b.AngularMomentum) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.AngularMomentum.CompareTo(b.AngularMomentum) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularMomentumDataset, UnitOfAngularMomentumDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfAngularMomentum a, UnitOfAngularMomentum b)
    {
        if (a.AngularMomentum > b.AngularMomentum)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.AngularMomentum < b.AngularMomentum)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.AngularMomentum == b.AngularMomentum)
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
