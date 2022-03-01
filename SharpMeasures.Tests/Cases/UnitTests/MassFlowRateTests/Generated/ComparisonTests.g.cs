#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfMassFlowRateTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassFlowRateDataset, UnitOfMassFlowRateDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfMassFlowRate a, UnitOfMassFlowRate b)
    {
        if (a.MassFlowRate.CompareTo(b.MassFlowRate) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.MassFlowRate.CompareTo(b.MassFlowRate) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassFlowRateDataset, UnitOfMassFlowRateDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfMassFlowRate a, UnitOfMassFlowRate b)
    {
        if (a.MassFlowRate > b.MassFlowRate)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.MassFlowRate < b.MassFlowRate)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.MassFlowRate == b.MassFlowRate)
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
