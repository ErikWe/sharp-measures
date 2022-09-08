#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfEnergyTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfEnergyDataset, UnitOfEnergyDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfEnergy a, UnitOfEnergy b)
    {
        if (a.Energy.CompareTo(b.Energy) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Energy.CompareTo(b.Energy) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfEnergyDataset, UnitOfEnergyDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfEnergy a, UnitOfEnergy b)
    {
        if (a.Energy > b.Energy)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Energy < b.Energy)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Energy == b.Energy)
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