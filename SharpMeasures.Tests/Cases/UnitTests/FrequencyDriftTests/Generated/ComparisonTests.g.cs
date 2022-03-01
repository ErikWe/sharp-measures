#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfFrequencyDriftTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDriftDataset, UnitOfFrequencyDriftDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfFrequencyDrift a, UnitOfFrequencyDrift b)
    {
        if (a.FrequencyDrift.CompareTo(b.FrequencyDrift) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.FrequencyDrift.CompareTo(b.FrequencyDrift) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfFrequencyDriftDataset, UnitOfFrequencyDriftDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfFrequencyDrift a, UnitOfFrequencyDrift b)
    {
        if (a.FrequencyDrift > b.FrequencyDrift)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.FrequencyDrift < b.FrequencyDrift)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.FrequencyDrift == b.FrequencyDrift)
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
