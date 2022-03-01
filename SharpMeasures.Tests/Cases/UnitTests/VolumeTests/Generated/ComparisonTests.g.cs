#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfVolumeTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumeDataset, UnitOfVolumeDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfVolume a, UnitOfVolume b)
    {
        if (a.Volume.CompareTo(b.Volume) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Volume.CompareTo(b.Volume) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumeDataset, UnitOfVolumeDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfVolume a, UnitOfVolume b)
    {
        if (a.Volume > b.Volume)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Volume < b.Volume)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Volume == b.Volume)
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
