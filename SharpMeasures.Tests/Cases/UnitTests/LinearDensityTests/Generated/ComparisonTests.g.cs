#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfLinearDensityTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLinearDensityDataset, UnitOfLinearDensityDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfLinearDensity a, UnitOfLinearDensity b)
    {
        if (a.LinearDensity.CompareTo(b.LinearDensity) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.LinearDensity.CompareTo(b.LinearDensity) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfLinearDensityDataset, UnitOfLinearDensityDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfLinearDensity a, UnitOfLinearDensity b)
    {
        if (a.LinearDensity > b.LinearDensity)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.LinearDensity < b.LinearDensity)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.LinearDensity == b.LinearDensity)
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
