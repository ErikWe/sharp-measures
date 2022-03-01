#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfTemperatureGradientTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureGradientDataset, UnitOfTemperatureGradientDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfTemperatureGradient a, UnitOfTemperatureGradient b)
    {
        if (a.TemperatureGradient.CompareTo(b.TemperatureGradient) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.TemperatureGradient.CompareTo(b.TemperatureGradient) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfTemperatureGradientDataset, UnitOfTemperatureGradientDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfTemperatureGradient a, UnitOfTemperatureGradient b)
    {
        if (a.TemperatureGradient > b.TemperatureGradient)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.TemperatureGradient < b.TemperatureGradient)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.TemperatureGradient == b.TemperatureGradient)
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
