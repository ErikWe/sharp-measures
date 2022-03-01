#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfSolidAngleTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSolidAngleDataset, UnitOfSolidAngleDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfSolidAngle a, UnitOfSolidAngle b)
    {
        if (a.SolidAngle.CompareTo(b.SolidAngle) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.SolidAngle.CompareTo(b.SolidAngle) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSolidAngleDataset, UnitOfSolidAngleDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfSolidAngle a, UnitOfSolidAngle b)
    {
        if (a.SolidAngle > b.SolidAngle)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.SolidAngle < b.SolidAngle)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.SolidAngle == b.SolidAngle)
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
