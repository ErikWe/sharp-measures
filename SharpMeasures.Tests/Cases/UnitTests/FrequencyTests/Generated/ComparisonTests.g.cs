#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfFrequencyTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDataset, UnitOfFrequencyDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfFrequency a, UnitOfFrequency b)
    {
        if (a.Frequency.CompareTo(b.Frequency) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Frequency.CompareTo(b.Frequency) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDataset, UnitOfFrequencyDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfFrequency a, UnitOfFrequency b)
    {
        if (a.Frequency > b.Frequency)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Frequency < b.Frequency)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Frequency == b.Frequency)
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
