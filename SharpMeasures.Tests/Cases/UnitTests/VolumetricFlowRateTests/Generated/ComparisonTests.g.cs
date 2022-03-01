#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfVolumetricFlowRateTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumetricFlowRateDataset, UnitOfVolumetricFlowRateDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfVolumetricFlowRate a, UnitOfVolumetricFlowRate b)
    {
        if (a.VolumetricFlowRate.CompareTo(b.VolumetricFlowRate) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.VolumetricFlowRate.CompareTo(b.VolumetricFlowRate) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfVolumetricFlowRateDataset, UnitOfVolumetricFlowRateDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfVolumetricFlowRate a, UnitOfVolumetricFlowRate b)
    {
        if (a.VolumetricFlowRate > b.VolumetricFlowRate)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.VolumetricFlowRate < b.VolumetricFlowRate)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.VolumetricFlowRate == b.VolumetricFlowRate)
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
