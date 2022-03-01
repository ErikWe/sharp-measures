#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfMassTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassDataset, UnitOfMassDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfMass a, UnitOfMass b)
    {
        if (a.Mass.CompareTo(b.Mass) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Mass.CompareTo(b.Mass) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfMassDataset, UnitOfMassDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfMass a, UnitOfMass b)
    {
        if (a.Mass > b.Mass)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Mass < b.Mass)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Mass == b.Mass)
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
