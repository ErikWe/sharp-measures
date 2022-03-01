#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfImpulseTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfImpulseDataset, UnitOfImpulseDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfImpulse a, UnitOfImpulse b)
    {
        if (a.Impulse.CompareTo(b.Impulse) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Impulse.CompareTo(b.Impulse) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfImpulseDataset, UnitOfImpulseDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfImpulse a, UnitOfImpulse b)
    {
        if (a.Impulse > b.Impulse)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Impulse < b.Impulse)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Impulse == b.Impulse)
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
