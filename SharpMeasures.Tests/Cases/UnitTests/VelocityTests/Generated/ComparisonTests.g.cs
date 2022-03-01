#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfVelocityTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocityDataset, UnitOfVelocityDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfVelocity a, UnitOfVelocity b)
    {
        if (a.Speed.CompareTo(b.Speed) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Speed.CompareTo(b.Speed) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVelocityDataset, UnitOfVelocityDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfVelocity a, UnitOfVelocity b)
    {
        if (a.Speed > b.Speed)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Speed < b.Speed)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Speed == b.Speed)
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
