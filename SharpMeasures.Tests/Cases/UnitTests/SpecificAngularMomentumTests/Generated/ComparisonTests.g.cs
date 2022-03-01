#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfSpecificAngularMomentumTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificAngularMomentumDataset, UnitOfSpecificAngularMomentumDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfSpecificAngularMomentum a, UnitOfSpecificAngularMomentum b)
    {
        if (a.SpecificAngularMomentum.CompareTo(b.SpecificAngularMomentum) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.SpecificAngularMomentum.CompareTo(b.SpecificAngularMomentum) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificAngularMomentumDataset, UnitOfSpecificAngularMomentumDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfSpecificAngularMomentum a, UnitOfSpecificAngularMomentum b)
    {
        if (a.SpecificAngularMomentum > b.SpecificAngularMomentum)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.SpecificAngularMomentum < b.SpecificAngularMomentum)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.SpecificAngularMomentum == b.SpecificAngularMomentum)
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
