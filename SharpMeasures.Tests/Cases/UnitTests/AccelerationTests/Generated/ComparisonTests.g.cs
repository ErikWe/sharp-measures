#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAccelerationTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAccelerationDataset, UnitOfAccelerationDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfAcceleration a, UnitOfAcceleration b)
    {
        if (a.Acceleration.CompareTo(b.Acceleration) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Acceleration.CompareTo(b.Acceleration) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAccelerationDataset, UnitOfAccelerationDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfAcceleration a, UnitOfAcceleration b)
    {
        if (a.Acceleration > b.Acceleration)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Acceleration < b.Acceleration)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Acceleration == b.Acceleration)
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
