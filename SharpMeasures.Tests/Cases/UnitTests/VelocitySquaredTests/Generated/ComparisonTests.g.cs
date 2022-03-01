#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfVelocitySquaredTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocitySquaredDataset, UnitOfVelocitySquaredDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfVelocitySquared a, UnitOfVelocitySquared b)
    {
        if (a.SpeedSquared.CompareTo(b.SpeedSquared) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.SpeedSquared.CompareTo(b.SpeedSquared) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocitySquaredDataset, UnitOfVelocitySquaredDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfVelocitySquared a, UnitOfVelocitySquared b)
    {
        if (a.SpeedSquared > b.SpeedSquared)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.SpeedSquared < b.SpeedSquared)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.SpeedSquared == b.SpeedSquared)
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
