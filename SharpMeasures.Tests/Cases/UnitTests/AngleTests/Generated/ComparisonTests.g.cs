#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfAngleTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngleDataset, UnitOfAngleDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfAngle a, UnitOfAngle b)
    {
        if (a.Angle.CompareTo(b.Angle) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Angle.CompareTo(b.Angle) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfAngleDataset, UnitOfAngleDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfAngle a, UnitOfAngle b)
    {
        if (a.Angle > b.Angle)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Angle < b.Angle)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Angle == b.Angle)
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
