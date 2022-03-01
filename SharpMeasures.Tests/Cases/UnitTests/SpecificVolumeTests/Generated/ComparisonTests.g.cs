#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfSpecificVolumeTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificVolumeDataset, UnitOfSpecificVolumeDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfSpecificVolume a, UnitOfSpecificVolume b)
    {
        if (a.SpecificVolume.CompareTo(b.SpecificVolume) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.SpecificVolume.CompareTo(b.SpecificVolume) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfSpecificVolumeDataset, UnitOfSpecificVolumeDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfSpecificVolume a, UnitOfSpecificVolume b)
    {
        if (a.SpecificVolume > b.SpecificVolume)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.SpecificVolume < b.SpecificVolume)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.SpecificVolume == b.SpecificVolume)
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
