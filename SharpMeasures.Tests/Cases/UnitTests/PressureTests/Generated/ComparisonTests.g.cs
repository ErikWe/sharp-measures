#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.UnitTests.UnitOfPressureTests;

using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Units;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPressureDataset, UnitOfPressureDataset>))]
    public void Method_ShouldMatchQuantity(UnitOfPressure a, UnitOfPressure b)
    {
        if (a.Pressure.CompareTo(b.Pressure) > 0)
        {
            Assert.True(a.CompareTo(b) > 0);
        }
        else if (a.Pressure.CompareTo(b.Pressure) < 0)
        {
            Assert.True(a.CompareTo(b) < 0);
        }
        else
        {
            Assert.Equal(0, a.CompareTo(b));
        }
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnitOfPressureDataset, UnitOfPressureDataset>))]
    public void Operator_ShouldMatchQuantity(UnitOfPressure a, UnitOfPressure b)
    {
        if (a.Pressure > b.Pressure)
        {
            Assert.True(a > b);
            Assert.True(a >= b);
            Assert.False(a <= b);
            Assert.False(a < b);
        }
        else if (a.Pressure < b.Pressure)
        {
            Assert.False(a > b);
            Assert.False(a >= b);
            Assert.True(a <= b);
            Assert.True(a < b);
        }
        else if (a.Pressure == b.Pressure)
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
