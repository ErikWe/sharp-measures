#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfDensityTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfDensityDataset, UnitOfDensityDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfDensity a, UnitOfDensity b)
    {
        if (a.Density.CompareTo(b.Density) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Density.CompareTo(b.Density) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfDensityDataset, UnitOfDensityDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfDensity a, UnitOfDensity b)
    {
        if (a.Density > b.Density)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Density < b.Density)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Density == b.Density)
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
