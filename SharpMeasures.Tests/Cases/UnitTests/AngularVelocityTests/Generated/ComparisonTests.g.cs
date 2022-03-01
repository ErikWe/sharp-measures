#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAngularVelocityTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularVelocityDataset, UnitOfAngularVelocityDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfAngularVelocity a, UnitOfAngularVelocity b)
    {
        if (a.AngularSpeed.CompareTo(b.AngularSpeed) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.AngularSpeed.CompareTo(b.AngularSpeed) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngularVelocityDataset, UnitOfAngularVelocityDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfAngularVelocity a, UnitOfAngularVelocity b)
    {
        if (a.AngularSpeed > b.AngularSpeed)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.AngularSpeed < b.AngularSpeed)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.AngularSpeed == b.AngularSpeed)
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
