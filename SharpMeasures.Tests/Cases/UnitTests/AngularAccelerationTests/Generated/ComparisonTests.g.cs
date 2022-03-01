#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAngularAccelerationTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularAccelerationDataset, UnitOfAngularAccelerationDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfAngularAcceleration a, UnitOfAngularAcceleration b)
    {
        if (a.AngularAcceleration.CompareTo(b.AngularAcceleration) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.AngularAcceleration.CompareTo(b.AngularAcceleration) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularAccelerationDataset, UnitOfAngularAccelerationDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfAngularAcceleration a, UnitOfAngularAcceleration b)
    {
        if (a.AngularAcceleration > b.AngularAcceleration)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.AngularAcceleration < b.AngularAcceleration)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.AngularAcceleration == b.AngularAcceleration)
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
