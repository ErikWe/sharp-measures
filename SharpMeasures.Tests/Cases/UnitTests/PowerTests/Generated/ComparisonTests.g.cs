#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfPowerTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPowerDataset, UnitOfPowerDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfPower a, UnitOfPower b)
    {
        if (a.Power.CompareTo(b.Power) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Power.CompareTo(b.Power) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPowerDataset, UnitOfPowerDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfPower a, UnitOfPower b)
    {
        if (a.Power > b.Power)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Power < b.Power)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Power == b.Power)
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
